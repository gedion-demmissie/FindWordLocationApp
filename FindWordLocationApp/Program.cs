using System;
using System.Collections.Generic;
using System.Linq;

namespace FindWordLocationApp
{
    /**
You're developing a system for scheduling advising meetings with students in a Computer Science program. Each meeting should be scheduled when a student has completed 50% of their academic program.

Each course at our university has at most one prerequisite that must be taken first. No two courses share a prerequisite. There is only one path through the program.

Write a function that takes a list of (prerequisite, course) pairs, and returns the name of the course that the student will be taking when they are halfway through their program. (If a track has an even number of courses, and therefore has two "middle" courses, you should return the first one.)

Sample input 1: (arbitrarily ordered)
pairs1 = [
     ["Foundations of Computer Science", "Operating Systems"],
     ["Data Structures", "Algorithms"],
     ["Computer Networks", "Computer Architecture"],
     ["Algorithms", "Foundations of Computer Science"],
     ["Computer Architecture", "Data Structures"],
     ["Software Design", "Computer Networks"]
]
// build dictionary 
//
In this case, the order of the courses in the program is:
     Software Design
     Computer Networks
     Computer Architecture
     Data Structures
     Algorithms
     Foundations of Computer Science
     Operating Systems

Sample output 1:
     "Data Structures"

Sample input 2:
pairs2 = [
     ["Algorithms", "Foundations of Computer Science"],
     ["Data Structures", "Algorithms"],
     ["Foundations of Computer Science", "Logic"],
     ["Logic", "Compilers"],
     ["Compilers", "Distributed Systems"],
]

Sample output 2:
     "Foundations of Computer Science"

Sample input 3:
pairs3 = [
     ["Data Structures", "Algorithms"],
]

Sample output 3:
     "Data Structures"

All Test Cases:
halfway_course(pairs1) => "Data Structures"
halfway_course(pairs2) => "Foundations of Computer Science"
halfway_course(pairs3) => "Data Structures"

Complexity analysis variables:

n: number of pairs in the input
*/

    class Solution
    {

        static string halfway_course(string[][] pairs)
        {
            Dictionary<string, string> prerequisiteAndCourseKV = new Dictionary<string, string>();
            Dictionary<string, string> courseAndPrerequisiteKV = new Dictionary<string, string>();
            int row = -1;
            for (row = 0; row < pairs.GetLength(0); row++)
            {
                prerequisiteAndCourseKV.Add(pairs[row][0], pairs[row][1]);
                courseAndPrerequisiteKV.Add(pairs[row][1], pairs[row][0]);
            }
            //Finding root course
            string courseWithNoPrerequisite = pairs[0][0];
            while (courseAndPrerequisiteKV.ContainsKey(courseWithNoPrerequisite))
            {
                courseWithNoPrerequisite = courseAndPrerequisiteKV[courseWithNoPrerequisite];

            }

            List<string> sortedCoursesByPreRequisite = new List<string>() { courseWithNoPrerequisite };
            var prerequisite = courseWithNoPrerequisite;
            for (row = 0; row < pairs.GetLength(0); row++)
            {
                sortedCoursesByPreRequisite.Add(prerequisiteAndCourseKV[prerequisite]);
                prerequisite = prerequisiteAndCourseKV[prerequisite];
            }
            int medianIndex = -1;
            if (sortedCoursesByPreRequisite.Count() % 2 == 0)
            {
                medianIndex = sortedCoursesByPreRequisite.Count() / 2 - 1;
            }
            else
            {
                medianIndex = sortedCoursesByPreRequisite.Count() / 2;
            }
            return sortedCoursesByPreRequisite.ElementAt(medianIndex);

        }



        static void Main(String[] args)
        {
            string[][] pairs1 = new[] {
        new [] {"Foundations of Computer Science", "Operating Systems"},
        new [] {"Data Structures", "Algorithms"},
        new [] {"Computer Networks", "Computer Architecture"},
        new [] {"Algorithms", "Foundations of Computer Science"},
        new [] {"Computer Architecture", "Data Structures"},
        new [] {"Software Design", "Computer Networks"}
        };

            string[][] pairs2 = new[] {
            new [] {"Algorithms", "Foundations of Computer Science"},
            new [] {"Data Structures", "Algorithms"},
            new [] {"Foundations of Computer Science", "Logic"},
            new [] {"Logic", "Compilers"},
            new [] {"Compilers", "Distributed Systems"},
        };

            string[][] pairs3 = new[] {
            new [] {"Data Structures", "Algorithms"}
        };

            Console.WriteLine(halfway_course(pairs1));
            Console.WriteLine(halfway_course(pairs2));
            Console.WriteLine(halfway_course(pairs3));

            Console.ReadKey();
        }
    }

}
