/*!
 * \file    PivotalService.cs 
 * \author  joselitofilhoo@gmail.com
 * \date    09/10/2011
 */
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using br.com.pivotaltracker.model;

namespace br.com.pivotaltracker.services
{
    /*!
     * 
     */
    public class PivotalService
    {
        #region Constructors
        
        /*!
         * 
         * \param token
         */
        public PivotalService(string token) 
        {
            Token = token;
            SaveToXml = false;
        }
        /*!
         * 
         * \param token
         * \param projectId
         */
        public PivotalService(string token, string projectId) 
        {
            Token = token;
            ProjectId = projectId;
            SaveToXml = false;
        }
        #endregion

        #region Getters/Setters
        //! 
        public bool SaveToXml { get; set; }
        //! 
        public string Token { get; set; }
        //! 
        public string ProjectId { get; set; }
        //! 
        public string StoryXmlDirectory { get; set; }
        #endregion

        #region Methods
        /*!
         * 
         * \param story
         * \return
         */
        public string AddStory(PivotalStory story)
        {
            string url = "http://www.pivotaltracker.com/services/v3/projects/" + ProjectId + "/stories?token=" + Token;

            string storyXml = "<story><story_type>" + story.StoryType + "</story_type><name>" + story.Name + "</name><requested_by>" + story.Requestor + "</requested_by>";
            if (!string.IsNullOrEmpty(story.Description)) storyXml += "<description>" + story.Description + "</description>";
            storyXml += "</story>";

            string response = SendRequest(url, storyXml);
            return "Pivotal story created: " + GetResponseByTag(response, "/story/url");
        }
        /*!
         * 
         * \param story
         * \param description
         * \return
         */
        public string AddTask(PivotalStory story, string description)
        {
            string url = "http://www.pivotaltracker.com/services/v3/projects/" + ProjectId + "/stories/" + story.StoryId + "/tasks?token=" + Token;

            string task = "<task><description>" + description + "</description></task>";

            string response = SendRequest(url, task);
            return "Pivotal story created: " + GetResponseByTag(response, "/story/url");
        }
        /*!
         * 
         * \param story
         * \param comment
         * \return
         */
        public string AddNote(PivotalStory story, string comment)
        {
            string url = "http://www.pivotaltracker.com/services/v3/projects/" + ProjectId + "/stories/" + story.StoryId + "/notes?token=" + Token;

            string note = "<note><text>" + comment + "</text></note>";

            string response = SendRequest(url, note);
            return "Pivotal note created: " + GetResponseByTag(response, "/note/id");
        }
        /*!
         * 
         * \param url
         * \param data
         * \return
         */
        private string SendRequest(string url, string data=null)
        {
            // Create a new HttpWebRequest object.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set the ContentType property.
            request.ContentType = "application/xml";

            // Set the Method property to 'POST' to post data to the URI.
            request.Method = "POST";
            request.Proxy = WebProxy.GetDefaultProxy();

            if (data != null)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                request.ContentLength = byteArray.Length;

                try
                {
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            Stream objStream;

            try
            {
                objStream = request.GetResponse().GetResponseStream();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            StreamReader objReader = new StreamReader(objStream);

            return StreamReader2String(objReader);
        }
        /*!
         * 
         * \param xml
         * \param tag
         * \return
         */
        private string GetResponseByTag(string xml, string tag)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            if (SaveToXml)
            {
                xmlDoc.Save(StoryXmlDirectory);
            }

            XmlNode urlNode = xmlDoc.SelectSingleNode(tag);
            return urlNode.FirstChild.InnerText ?? "(couldn't load url)";
        }
        /*!
         * 
         * \param project
         * \return
         */
        public string GetStories(string project)
        {
            string url = "http://www.pivotaltracker.com/services/v3/projects/" + ProjectId + "/stories?token=" + Token;

            return SendRequest(url);
        }
        /*!
         * 
         * \param objReader
         * \return
         */
        private string StreamReader2String(StreamReader objReader)
        {
            string sLine;
            StringBuilder sb = new StringBuilder();

            while ((sLine = objReader.ReadLine()) != null)
            {
                sb.Append(sLine);
            }

            return sb.ToString();
        }
        #endregion
    }
}

