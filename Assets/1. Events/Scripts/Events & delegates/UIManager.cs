using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI playerKillCountText;
    private PlayerLogic playerLogic; 
    private void Start() {
        playerLogic = Object.FindObjectOfType<PlayerLogic>();
        playerLogic.playerDeathDelegate += updateUI;
    }
    private void updateUI() {
        playerScoreText.text = ((int)playerLogic.playerScore).ToString();
        playerKillCountText.text = ((int)playerLogic.playerKillCount).ToString();
    }
}
