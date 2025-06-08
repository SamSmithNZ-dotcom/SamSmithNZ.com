using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class GoalDataAccess : BaseDataAccess<Goal>, IGoalDataAccess
    {
        public GoalDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Goal>> GetListByGame(int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetList("FB_GetGoals", parameters);
        }

        public async Task<List<Goal>> GetList()
        {
            return await base.GetList("FB_GetGoals");
        }

        public async Task<bool> SaveItem(Goal goal)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GoalCode", goal.GoalCode, DbType.Int32);
            parameters.Add("@GameCode", goal.GameCode, DbType.Int32);
            parameters.Add("@PlayerCode", goal.PlayerCode, DbType.Int32);
            parameters.Add("@GoalTime", goal.GoalTime, DbType.Int32);
            parameters.Add("@InjuryTime", goal.InjuryTime, DbType.Int32);
            parameters.Add("@IsPenalty", goal.IsPenalty, DbType.Boolean);
            parameters.Add("@IsOwnGoal", goal.IsOwnGoal, DbType.Boolean);
            parameters.Add("@IsGoldenGoal", goal.IsGoldenGoal, DbType.Boolean);

            return await base.SaveItem("FB_SaveGoal", parameters);
        }

        public async Task<bool> DeleteItem(Goal goal)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GoalCode", goal.GoalCode, DbType.Int32);

            return await base.SaveItem("FB_DeleteGoal", parameters);
        }

        public List<Goal> ProcessGoalHTMLMigration(string goalText, string playerName)
        {
            List<Goal> results = new();
            if (string.IsNullOrEmpty(goalText) == false)
            {
                goalText = goalText.Replace(playerName, "");
                goalText = goalText.Replace("&#39;", " ");
                goalText = goalText.Replace("&#160;", " ");
                string[] goals;
                if (goalText.IndexOf(',') >= 0)
                {
                    goals = goalText.Split(',');
                }
                else
                {
                    //Initialize a single item array
                    goals = new string[1];
                    goals[0] = goalText;
                }
                foreach (string goalItem in goals)
                {
                    string goalItemText = goalItem;
                    Goal newGoal = new();
                    //The goal can be a variety of formats, but most often, just 90' - so we try that first (the ' is stripped off by the previous line)
                    int goalTime;
                    int injuryTime = 0;
                    if (int.TryParse(goalItemText, out goalTime) == false)
                    {
                        //It's not a regular goal, let's look at the special situations (penalties, own goals, etc)
                        //Penalties
                        if (goalItemText.IndexOf("(pen.)") >= 0)
                        {
                            goalItemText = goalItemText.Replace("(pen.)", "");
                            newGoal.IsPenalty = true;
                        }
                        //Own Goals
                        else if (goalItemText.IndexOf("(o.g.)") >= 0)
                        {
                            goalItemText = goalItemText.Replace("(o.g.)", "");
                            newGoal.IsOwnGoal = true;
                        }
                        if (int.TryParse(goalItemText, out goalTime) == false)
                        {
                            //Extra/injury time
                            if (goalItemText.IndexOf("+") >= 0)
                            {
                                string[] injuryTimeGoals = goalItemText.Split("+");
                                if (injuryTimeGoals.Length == 2)
                                {
                                    _ = int.TryParse(injuryTimeGoals[0], out goalTime);
                                    _ = int.TryParse(injuryTimeGoals[1], out injuryTime);
                                }
                            }
                        }
                    }

                    newGoal.GoalTime = goalTime;
                    newGoal.InjuryTime = injuryTime;
                    results.Add(newGoal);
                }
            }

            return results;
        }

    }
}