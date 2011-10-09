/*!
 * \file    PivotalTask.cs 
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
    public class PivotalTask
    {
        public string StoryId { get; set; }

        public int TaskId { get; set; }

        public int Position { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public DateTime CreationDate { get; set; }

        public List<PivotalTask> Tasks { get; set; }
    }

}
