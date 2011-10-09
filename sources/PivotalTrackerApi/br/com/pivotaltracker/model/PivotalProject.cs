/*!
 * \file    PivotalProject.cs 
 * \author  joselitofilhoo@gmail.com
 * \date    09/10/2011
 */
﻿using System.Collections.Generic;
using System.Linq;

namespace br.com.pivotaltracker.model
{
    /*!
     * 
     */
    public class PivotalProject
    {
        public List<PivotalStory> Stories { get; set; }

        public List<PivotalStory> Bugs
        {
            get
            {
                return Stories.Where(x => x.StoryType == PivotalStoryType.bug).ToList();
            }
        }

        public List<PivotalStory> Chores
        {
            get
            {
                return Stories.Where(x => x.StoryType == PivotalStoryType.chore).ToList();
            }
        }

        public List<PivotalStory> Features
        {
            get
            {
                return Stories.Where(x => x.StoryType == PivotalStoryType.feature).ToList();
            }
        }
    }
}