using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeFour.Models;

namespace TicTacToeFour.Data
{
    public class DataServiceSeed : IDataService
    {
        public List<Player> ReadAll()
        {
            return new List<Player>()
            {
                new Player()
                {
                    Name = "Player X",
                    Wins = 0,
                    Losses = 0,
                    Ties = 0
                },

                new Player()
                {
                    Name = "Player O",
                    Wins = 0,
                    Losses = 0,
                    Ties = 0
                }
            };
        }

        public void WriteAll(List<Player> players)
            {
                //
            }
        }
    }
