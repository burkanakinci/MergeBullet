using UnityEngine;
using System.Collections.Generic;
public class JsonConverter : CustomBehaviour
{
    private string m_TempJsonData;
    public override void Initialize()
    {
    }
    public void LoadPlayerData(ref PlayerData _playerData)
    {
        var data = PlayerPrefs.GetString(Constant.PLAYER_DATA);

        if (string.IsNullOrEmpty(data))
        {

            _playerData = new PlayerData
            {
                PlayerLevel = 1,
                FilledNodesX = new List<int>(),
                FilledNodesY = new List<int>(),
                FilledNodesLevel = new List<int>(),
                PlayerCoin = 100,
            };

            SavePlayerData(_playerData);
        }
        else
        {
            _playerData = JsonUtility.FromJson<PlayerData>(data);
        }
    }
    public void SavePlayerData(PlayerData _playerData)
    {
        m_TempJsonData = JsonUtility.ToJson(_playerData);
        PlayerPrefs.SetString((Constant.PLAYER_DATA), (m_TempJsonData));
        PlayerPrefs.Save();
    }
}
