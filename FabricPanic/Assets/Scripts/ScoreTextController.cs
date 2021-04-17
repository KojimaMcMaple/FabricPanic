using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score_text_;
    private ScoreManager score_manager_;

    // Start is called before the first frame update
    void Start()
    {
        score_manager_ = transform.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text_.text = score_manager_.GetGameScore().ToString();
    }
}
