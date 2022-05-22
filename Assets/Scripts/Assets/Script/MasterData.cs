using System.Collections.Generic;
public partial class MasterData
{
	public Dictionary<ulong, LocalizeCommonControllerData> LocalizeCommonControllerData = new Dictionary<ulong, LocalizeCommonControllerData>();
	public Dictionary<ulong, LocalizeRolesData> LocalizeRolesData = new Dictionary<ulong, LocalizeRolesData>();
	public Dictionary<ulong, LocalizeSkillsData> LocalizeSkillsData = new Dictionary<ulong, LocalizeSkillsData>();

}
