using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateSystem : MonoBehaviour
{
    private List<IUltimate> ultimateSkill;
    
    public Slider UltiMeter;
    private PlayerMeleeatk meleeattack;
    [Header("Ultimate")]
    [SerializeField] int maxUltiCounter = 5;
    [SerializeField] int ultiCounter = 0;

    public List<GameObject> SkillObj;
    [Header("Ulti Color")]
    [SerializeField] Color initialColor;
    [SerializeField] Color filledColor;
    [SerializeField] List<Image> imagesToColour;
    private bool colorChanged;
    private void Awake()
    {
        meleeattack = GetComponent<PlayerMeleeatk>();
        UltiMeter.maxValue = maxUltiCounter;
        UltiMeter.value = ultiCounter;

        //fix nanti
        ultimateSkill = new List<IUltimate>();
        ultimateSkill.Add(SkillObj[0].GetComponent<IUltimate>());
        ultimateSkill.Add(SkillObj[1].GetComponent<IUltimate>());

        ChangeUIColour(false);
    }

    private void ChangeUIColour(bool filled)
    {
        foreach (Image img in imagesToColour)
        {
            if (!filled)
                img.color = initialColor;
            else
                img.color = filledColor;
        }

        if (!filled) colorChanged = false;
    }

    public void FillUltimate(int amount)
    {
        ultiCounter = Mathf.Min(ultiCounter + amount, maxUltiCounter);
        UltiMeter.value = ultiCounter;
        if (ultiCounter == maxUltiCounter && !colorChanged)
        {
            colorChanged = true;
            ChangeUIColour(true);
        }
        
    }

    public void UseUltimate(Vector2 position , int index)
    {
        if (ultiCounter < maxUltiCounter) return;
        ultimateSkill[index].InvokeUltimate(position);
        UltiMeter.value = 0;
        ultiCounter = 0;
        ChangeUIColour(false);

    }
}

public interface IUltimate
{
    void InvokeUltimate(Vector2 mousePos);

}
