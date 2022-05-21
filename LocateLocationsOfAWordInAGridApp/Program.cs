using System;
using System.Collections.Generic;
using System.Text;

namespace LocateLocationsOfAWordInAGridApp
{
    /*
    * You are running a classroom and suspect that some of your students are passing around the answers
    * to multiple choice questions.
    * 
    * After catching your classroom students cheating before, you realize your students are getting craftier and 
    * hiding words in 2D grids of letters. The word may start anywhere in the grid, and consecutive letters can be
    * either immediately below or immediately to the right of the previous letter.
    * 
    * Given a grid and a word, write a function that returns the location of the word in the grid as a list of 
    * coordinates. If there are multiple matches, return any one.
    * 
    * grid1 =  [
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x'],
    *     ['c', 'c', 't', 'n', 'a','x']
    * ]
    * word1 = "catnip"
    * word2 = "cccc"
    * word3 = "s"
    * word4 = "ant"
    * word5 = "aoi"
    * word6 = "ki"
    * word7 = "aaoo"
    * word8 = "ooo"
    * 
    * grid2 = [['a']]
    * word9= "a"
    * 
    * find_word_location(grid1, word1) => [(1,1), (1,2),(1,3),(2,3),(3,3),(3,4)]
    * find_word_location(grid1, word2) => [(0,0), (1,0),(1,1),(2,1)] OR [(0,0),(0,1), (1,1),(2,1)]   
    * find_word_location(grid1, word3) => [(5,0)]
    * find_word_location(grid1, word4) => [(0,4), (1,4),(2,4)] OR [(0,4),(1,4),(1,5)]
    * find_word_location(grid1, word5) => [(4,5), (5,5),(6,5)] 
    * find_word_location(grid1, word6) => [(6,4), (6,5)] 
    * find_word_location(grid1, word7) => [(5,2), (5,3),(5,4),(5,5)] 
    * find_word_location(grid1, word8) => [(4,1), (4,2),(4,3)] 
    * find_word_location(grid2, word9) => [(0,0)]
    * 
    * Complexity analysis variables:
    * 
    * r = number of rows
    * c = number of columns
    * w = length of the word
    */
    class Coordinate
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
      

        public Coordinate(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }
    class Solution
    {
        //static Coordinate[] find_word_location(char[][] grid1, string word1)
        //{
        //    string matchedWord = string.Empty;
        //    int indexOfCurentMatchWord = 0;

        //    int rowIndex = 0;
        //    int colIndex = 0;
        //    List<Coordinate> matchedWordLocations = new List<Coordinate>();


        //    while (rowIndex < grid1.GetLength(0) && colIndex < grid1.GetLength(1))
        //    {
        //        if (word1[indexOfCurentMatchWord] == grid1[rowIndex][colIndex])
        //        {
        //            indexOfCurentMatchWord++;
        //            matchedWordLocations.Add(new Coordinate { RowIndex = rowIndex, ColumnIndex = colIndex });
        //            while (indexOfCurentMatchWord < word1.Length)
        //            {
        //                //both right and bottom match
        //                if (word1[indexOfCurentMatchWord] == grid1[rowIndex][colIndex + 1] && word1[indexOfCurentMatchWord] == grid1[rowIndex + 1][colIndex])
        //                {
        //                    indexOfCurentMatchWord++;
        //                    matchedWordLocations.Add(new Coordinate { RowIndex = rowIndex, ColumnIndex = colIndex + 1 });
        //                    colIndex++;
        //                    if (colIndex == grid1.GetLength(1))
        //                    {

        //                        break;
        //                    }
        //                }
        //                else if (word1[indexOfCurentMatchWord] == grid1[rowIndex][colIndex + 1]) //only right match 
        //                {
        //                    indexOfCurentMatchWord++;
        //                    matchedWordLocations.Add(new Coordinate { RowIndex = rowIndex, ColumnIndex = colIndex + 1 });
        //                    colIndex++;
        //                    if (colIndex == grid1.GetLength(1))
        //                    {
        //                        break;
        //                    }
        //                }
        //                else if (word1[indexOfCurentMatchWord] == grid1[rowIndex + 1][colIndex]) // only bottom match
        //                {
        //                    indexOfCurentMatchWord++;
        //                    matchedWordLocations.Add(new Coordinate { RowIndex = rowIndex + 1, ColumnIndex = colIndex });
        //                    rowIndex++;
        //                    if (colIndex == grid1.GetLength(1))
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            colIndex++;
        //            if (colIndex == grid1.GetLength(1) && rowIndex < grid1.GetLength(0))
        //            {
        //                rowIndex += 1;
        //                if(rowIndex == grid1.GetLength(0))
        //                {
        //                    break;
        //                }
        //                colIndex = 0;
        //            }
        //        }

        //    }

        //}

        static string find_word_location(char[][] charactersGrid, string word)
        {
          int rowIndex = 0;
          int columnIndex = 0;
          StringBuilder sb = new StringBuilder();
          for (rowIndex = 0; rowIndex < charactersGrid.GetLength(0); rowIndex++)
          {
                for (columnIndex = 0; columnIndex < charactersGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    var returnedCoordinates = matchWordsByInitialPostion(charactersGrid, word, rowIndex, columnIndex).ToArray();
                    if (returnedCoordinates.Length > 0)
                    {
                        var NumberOfMatchingWordLocations = returnedCoordinates.Length / word.Length;
                        for (int i = 1; i <= NumberOfMatchingWordLocations; i++)
                        {
                            sb.Append("[");
                            sb.Append($"({returnedCoordinates[(i - 1) * word.Length].RowIndex}, {returnedCoordinates[0].ColumnIndex})");
                            for (int j = (i - 1) * word.Length + 1; j < i * word.Length; j++)
                            {
                                sb.Append($", ({returnedCoordinates[j].RowIndex}, {returnedCoordinates[j].ColumnIndex})");
                            }
                            sb.Append("]");

                            if (i < NumberOfMatchingWordLocations)
                            {
                                sb.Append(" OR ");
                            }
                        }
                    }

                }
          }
         

          return $"find_word_location(charactersGrid, {word}) => " + (sb.Length > 0 ?  sb.ToString() : "[]");
        }

        static List<Coordinate> matchWordsByInitialPostion(char[][] charactersGrid, string word, int rowIndex, int columnIndex)
        {
            var matchedCoordinateListRight = new List<Coordinate>();
            var matchedCoordinateListBottom = new List<Coordinate>();
            //var targetLetterIndex = 0;
            var targetLetter = word[0];
            if (word.Length >= 1)
            {
                var matchedCoordinateListItem = matchCharacterAtSpecifiedLocation(charactersGrid, targetLetter, rowIndex, columnIndex);

                if (matchedCoordinateListItem != null)
                {
                    matchedCoordinateListRight.Add(matchedCoordinateListItem);
                    matchedCoordinateListBottom.Add(matchedCoordinateListItem);
                    if(word.Length == 1)
                    {
                        return matchedCoordinateListRight;
                    }

                    //next target match
                    targetLetter = word[1];

                    //Find the next right match
                    var matchedCoordinateListItemFromRight = matchCharacterAtSpecifiedLocation(charactersGrid, targetLetter, rowIndex, columnIndex + 1);
                    if (matchedCoordinateListItemFromRight != null)
                    {
                        var matchedPortionFromRight = matchWordsByInitialPostion(charactersGrid, word.Substring(1), rowIndex, columnIndex + 1);
                        matchedCoordinateListRight.AddRange(matchedPortionFromRight);                    
                    }

                    //Find the next bottom match
                    var matchedCoordinateListItemFromBottom = matchCharacterAtSpecifiedLocation(charactersGrid, targetLetter, rowIndex + 1, columnIndex);
                    if(matchedCoordinateListItemFromBottom != null)
                    {                        
                        var matchedPortionFromBottom = matchWordsByInitialPostion(charactersGrid, word.Substring(1), rowIndex + 1, columnIndex);
                        matchedCoordinateListBottom.AddRange(matchedPortionFromBottom);
                    }

                    //Merge matched values selectively if both neighbors are matched
                    if(matchedCoordinateListRight.Count == word.Length && matchedCoordinateListBottom.Count == word.Length)
                    {
                        matchedCoordinateListRight.AddRange(matchedCoordinateListBottom);
                        return matchedCoordinateListRight;
                    }
                    else if (matchedCoordinateListRight.Count == word.Length)
                    {
                        return matchedCoordinateListRight;
                    }
                    else if (matchedCoordinateListBottom.Count == word.Length)
                    {
                        return matchedCoordinateListBottom;
                    }
                    else if(matchedCoordinateListRight.Count == word.Length * 2 - 1 && matchedCoordinateListBottom.Count < word.Length)
                    {
                        //if there were common juction that merged the last few scanned letters
                        //For consistency we precede the right direction over the bottom

                        for (int i = word.Length; i < word.Length * 2 - 1; i++)
                        {
                            matchedCoordinateListBottom.Add(matchedCoordinateListRight[i]);
                        }
                        
                        matchedCoordinateListRight.RemoveRange(word.Length, word.Length - 1);

                         matchedCoordinateListRight.AddRange(matchedCoordinateListBottom);
                        return matchedCoordinateListRight;
                    }
                    else if (matchedCoordinateListBottom.Count == word.Length * 2 - 1 && matchedCoordinateListRight.Count < word.Length)
                    {
                        //if there were common juction that merged the last few scanned letters
                        for (int i = word.Length; i < word.Length * 2 - 1; i++)
                        {
                            matchedCoordinateListRight.Add(matchedCoordinateListBottom[i]);
                        }

                        matchedCoordinateListBottom.RemoveRange(word.Length, word.Length - 1);

                         matchedCoordinateListRight.AddRange(matchedCoordinateListBottom);
                        return matchedCoordinateListRight;
                    }
                }
            }      
            return new List<Coordinate>();
        }

        static Coordinate matchCharacterAtSpecifiedLocation(char[][] charactersGrid, char character, int rowIndex, int columnIndex)
        {
            if(rowIndex < charactersGrid.GetLength(0) && columnIndex < charactersGrid[rowIndex].GetLength(0) && charactersGrid[rowIndex][columnIndex] == character)
            {
                return new Coordinate(rowIndex, columnIndex);
            }
            return null;
        }
        static void Main(String[] args)
        {

            char[][] grid1 = new[] {
            new []{'c', 'c', 't', 'n', 'a', 'x'},
            new []{'c', 'c', 'a', 't', 'n', 't'},
            new []{'a', 'c', 'n', 'n', 't', 't'},
            new []{'t', 'n', 'i', 'i', 'p', 'p'},
            new []{'a', 'o', 'o', 'o', 'a', 'a'},
            new []{'s', 'a', 'a', 'a', 'o', 'o'},
            new []{'k', 'a', 'i', 'o', 'k', 'i'},
        };
            string word1 = "catnip";
            string word2 = "cccc";
            string word3 = "s";
            string word4 = "ant";
            string word5 = "aoi";
            string word6 = "ki";
            string word7 = "aaoo";
            string word8 = "ooo";

            char[][] grid2 = new[] { new[] { 'a' } };
            string word9 = "a";

            Console.WriteLine(find_word_location(grid1, word1));
            Console.WriteLine(find_word_location(grid1, word2));
            Console.WriteLine(find_word_location(grid1, word3));
            Console.WriteLine(find_word_location(grid1, word4));
            Console.WriteLine(find_word_location(grid1, word5));
            Console.WriteLine(find_word_location(grid1, word6));
            Console.WriteLine(find_word_location(grid1, word7));
            Console.WriteLine(find_word_location(grid1, word8));
            Console.WriteLine(find_word_location(grid2, word9));
            Console.ReadKey();
        }
    }
}
