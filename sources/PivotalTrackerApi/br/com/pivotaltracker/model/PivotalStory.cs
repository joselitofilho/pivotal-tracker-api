/*!
 * \file    PivotalStory.cs 
 * \author  joselitofilhoo@gmail.com
 * \date    09/10/2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.com.pivotaltracker.model
{
    /*!
     * 
     */
    public enum PivotalStoryType
    {
        feature,
        chore,
        bug
    }

    /*!
     * 
     */
    public class PivotalStory
    {
        public PivotalStoryType StoryType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Requestor { get; set; }

        public string Labels { get; set; }

        public int Estimate { get; set; }

        public string Url { get; set; }

        //! \remark
        [System.Xml.Serialization.XmlElementAttribute("current_state")]
        public string CurrentState { get; set; }

        //! \remark
        [System.Xml.Serialization.XmlElementAttribute("creation_date", DataType = "date")]
        public DateTime CreationDate { get; set; }

        public int StoryId { get; set; }
    }
}
