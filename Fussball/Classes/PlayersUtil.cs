using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fussball.SimplePointsSystem;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for PlayersUtil
/// </summary>
public class PlayersUtil
{
    static readonly object padlock = new object();

    public PlayersUtil()
    {
    }

    #region league

    public static void LoadLeague()
    {
        lock (padlock)
        {
            LeagueMatches matches = null;
            Players players = null;

            string matchespath = ConfigurationManager.AppSettings["leaguematchesfile"];
            string playerspath = ConfigurationManager.AppSettings["leagueplayersfile"];

            if (File.Exists(matchespath))
            {
                matches = LeagueMatchesXML.CreateLeagueMatchesFromXml(ReadTextFile(matchespath));
            }
            else
            {
                using (FileStream fs = File.Create(matchespath)) { }
                matches = new LeagueMatches();
                SaveFile(LeagueMatchesXML.ToXml(matches), matchespath);
            }

            if (File.Exists(playerspath))
            {
                players = PlayersXML.CreatePlayersFromXml(ReadTextFile(playerspath));
            }
            else
            {
                using (FileStream fs = File.Create(playerspath)) { }
                players = new Players();
                SaveFile(PlayersXML.ToXml(players), playerspath);
            }

            League.Instance = new League(matches, players);
        }
    }

    public static void SaveLeague()
    {
        lock (padlock)
        {
            if (League.Instance != null)
            {
                string matchespath = ConfigurationManager.AppSettings["leaguematchesfile"];
                string playerspath = ConfigurationManager.AppSettings["leagueplayersfile"];
                SaveFile(LeagueMatchesXML.ToXml(League.Instance.Matches), matchespath);
                SaveFile(PlayersXML.ToXml(League.Instance.Players), playerspath);
            }
        }
    }

    #endregion

    #region players

    public static Players ThePlayers
    {
        get
        {
            if (HttpContext.Current.Application["players"] == null)
            {
                LoadPlayersFile();
            }
            return HttpContext.Current.Application["players"] as Players;
        }
        set
        {
            HttpContext.Current.Application["players"] = value;
        }
    }

    public static void SavePlayersFile()
    {
        lock (padlock)
        {
            string path = ConfigurationManager.AppSettings["playersfile"];
            SaveThePlayers(path);
        }
    }

    public static void LoadPlayersFile()
    {
        lock (padlock)
        {
            string path = ConfigurationManager.AppSettings["playersfile"];
            if (File.Exists(path))
            {                
                Players pTemp = PlayersXML.CreatePlayersFromXml(ReadTextFile(path));

                ThePlayers = pTemp;

            }
            else
            {
                
                using (FileStream fs = File.Create(path)) { }                

                ThePlayers = new Players();
                SaveThePlayers(path);
            }
        }
    }

    private static void SaveThePlayers(string path)
    {
        SaveFile(PlayersXML.ToXml(ThePlayers), path);
    }

    #endregion

    #region audit trail

    public static void LoadAuditTrail()
    {
        lock (padlock)
        {
            string path = ConfigurationManager.AppSettings["audittrailfile"];
            if (File.Exists(path))
            {                
                AuditTrail.Instance = AuditTrailXML.CreateAuditTrailFromXml(ReadTextFile(path));
            }
            else
            {
                using (FileStream fs = File.Create(path)) { }
                AuditTrail.Instance = new AuditTrail();
                SaveAuditTrailFile(path);
            }
        }
    }

    public static void SaveAuditTrail()
    {
        lock (padlock)
        {
            string path = ConfigurationManager.AppSettings["audittrailfile"];
            SaveAuditTrailFile(path);
        }
    }

    private static void SaveAuditTrailFile(string path)
    {
        SaveFile(AuditTrailXML.ToXml(AuditTrail.Instance), path);
    }

    #endregion

    #region generic file operations
    internal static string ReadTextFile(string path)
    {
        StringBuilder buffer = new StringBuilder();
        using (StreamReader reader = File.OpenText(path))
        {
            string input;

            while ((input = reader.ReadLine()) != null)
            {
                buffer.Append(input);
            }
        }
        return buffer.ToString();
    }

    internal static void SaveFile(string fileContent, string filePath)
    {
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.Write(fileContent);
            writer.Flush();
        }
    }
    #endregion
}
