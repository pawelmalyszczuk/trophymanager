using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyManagerNew
{
    class ContextBroker
    {
        #region Player

        public Player getPlayerById(string playerId)
        {
            using (var context = new ModelTrophy())
            {
                var player = from playerDict in context.Players
                             where playerDict.TrophyCode == playerId
                             select playerDict;

                if (player.Any())
                    return player.First();
            }
            return null;
        }

        public IEnumerable<Player> getPlayers()
        {
            using (var context = new ModelTrophy())
            {
                var player = from playerDict in context.Players
                             orderby playerDict.PositionCode
                             select playerDict;

                if (player.Any())
                    return player.ToList();
            }
            return null;
        }

        public void savePlayer(int playerId, string playerName, string playerPositionCode, string playerHW)
        {
            using (var context = new ModelTrophy())
            {
                context.Players.Add(new Player { Id = playerId, Name = playerName, PositionCode = playerPositionCode, TrophyCode = playerHW });
                context.SaveChanges();
            }
        }

        #endregion

        # region Version

        public Version getVersionByFileName(string fileName)
        {
            using (var context = new ModelTrophy())
            {
                var version = from versionList in context.Versions
                              where versionList.FileName == fileName
                              select versionList;

                if (version.Any())
                    return version.First();
            }
            return null;
        }

        public Version getVersionLast()
        {
            using (var context = new ModelTrophy())
            {
                var version = from versionList in context.Versions
                              orderby versionList.DateWeekYear descending
                              select versionList;

                if (version.Any())
                    return version.First();
            }
            return null;
        }

        public Version getVersionBeforeLast()
        {
            using (var context = new ModelTrophy())
            {
                var version = from versionList in context.Versions
                              orderby versionList.DateWeekYear descending
                              select versionList;

                if (version.Any())
                    if (version.Count() > 1)
                        return version.Skip(1).First();
            }
            return null;
        }

        public void saveVersion(DateTime versionDate, string fileName, string weekYear)
        {
            using (var context = new ModelTrophy())
            {
                context.Versions.Add(new Version { Data = versionDate, FileName = fileName, DateWeekYear = weekYear });
                context.SaveChanges();
            }
        }

        #endregion

        #region SkillDictionary

        public bool isSkillInDictionary(ModelTrophy context, string skillCode)
        {
            var skillD = from skillDict in context.SkillsDictionary
                         where skillDict.SkillCode == skillCode
                         select skillDict;

            if (skillD.Any())
                return true;

            return false;
        }

        public long getIdSkillInDictionary(ModelTrophy context, string skillCode)
        {
            var skillD = from skillDict in context.SkillsDictionary
                         where skillDict.SkillCode == skillCode
                         select skillDict;

            if (skillD.Any())
                return skillD.First().Id;

            return 0;
        }

        public void saveSkillInDictionary(int skillId, string skillCode, string skillName, string skillVisName)
        {
            using (var context = new ModelTrophy())
            {
                if (!isSkillInDictionary(context, skillCode))
                {
                    // save skill to dictionary
                    context.SkillsDictionary.Add(new SkillDictionary { Id = skillId, SkillCode = skillCode, SkillName = skillName, SkillVisName = skillVisName });
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<SkillDictionary> getSkillsInDictionary()
        {
            using (var context = new ModelTrophy())
            {
                var skill = from skillDict in context.SkillsDictionary
                            select skillDict;

                if (skill.Any())
                    return skill.ToList();
            }

            return null;
        }

        #endregion

        #region Skill

        public bool isSkillInDB(long playerId, long versionId, long skillDictId)
        {
            using (var context = new ModelTrophy())
            {
                var skill = from skills in context.Skills
                            where skills.VerId == versionId && skills.SkillDictId == skillDictId && skills.PlayerId == playerId
                            select skills;

                if (skill.Any())
                    return true;
            }

            return false;
        }

        public void saveSkill(long playerId, long versionId, string skillCode, double skillValue)
        {
            using (var context = new ModelTrophy())
            {
                long idSkill = getIdSkillInDictionary(context, skillCode);

                if (idSkill > 0)
                {
                    // Check skill in DB
                    if (!isSkillInDB(playerId, versionId, idSkill))
                    {
                        context.Skills.Add(new Skill { SkillDictId = idSkill, SkillValue = skillValue, PlayerId = playerId,  VerId = versionId });
                        context.SaveChanges();
                    }
                }
            }
        }

        public double getSkillValue(long playerId, long versionId, string skillCode)
        {
            using (var context = new ModelTrophy())
            {
                long idSkill = getIdSkillInDictionary(context, skillCode);
                if (idSkill > 0)
                {
                    var skill = from skills in context.Skills
                                where skills.VerId == versionId && skills.SkillDictId == idSkill && skills.PlayerId == playerId
                                select skills;

                    if (skill.Any())
                        return skill.First().SkillValue;
                }
            }

            return 0;
        }

        #endregion
    }
}
