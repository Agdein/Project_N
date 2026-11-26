using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;

public class UnitVisualController : MonoBehaviour
{
    [Header("Ссылки на скелеты")]
    public SkeletonAnimation skeletonCombat;
    public SkeletonAnimation skeletonAttack;
    public SkeletonAnimation skeletonFX;
    public Button Button;

    void Start()
    {
        ShowCombat();
        
        Button.onClick.AddListener(() => PlayFirstAnimation(skeletonAttack, skeletonFX, true) );
    }

    public void ShowCombat()
    {
        SetActiveSkeletons(skeletonCombat);
    }

    // --- МЕТОД 1: Для одной пары ---
    // Вместо имени анимации мы просто передаем скелет
    public void PlayFirstAnimation(SkeletonAnimation targetSkeleton, bool returnToCombat = true)
    {
        if (targetSkeleton == null) return;

        // Получаем имя первой анимации из скелета
        string animName = GetFirstAnimationName(targetSkeleton);
        
        // Запускаем (используя внутренний метод или логику)
        PlayInternal(targetSkeleton, animName, returnToCombat);
    }

    // --- МЕТОД 2: Для двух пар (перегрузка) ---
    public void PlayFirstAnimation(SkeletonAnimation skeletonA, SkeletonAnimation skeletonB, bool returnToCombat = true)
    {
        // Первый скелет (атака) - просто включаем, он не будет возвращать стойку сам
        if (skeletonA != null)
        {
            string animA = GetFirstAnimationName(skeletonA);
            PlayInternal(skeletonA, animA, false);
        }

        // Второй скелет (FX) - именно он будет отвечать за возврат в стойку
        if (skeletonB != null)
        {
            string animB = GetFirstAnimationName(skeletonB);
            PlayInternal(skeletonB, animB, returnToCombat);
        }
        else
        {
            // Если FX нет, то первая пара (атака) должна вернуть стойку
            if (returnToCombat && skeletonA != null)
            {
                 ShowCombat(); // Или подписаться на завершение атаки
            }
        }
    }

    // --- Вспомогательный метод для получения имени первой анимации ---
    private string GetFirstAnimationName(SkeletonAnimation skeleton)
    {
        if (skeleton == null || skeleton.Skeleton.Data == null) return null;
        
        // Skeleton.Data.Animations - это список всех анимаций в скелете
        if (skeleton.Skeleton.Data.Animations.Count > 0)
        {
            return skeleton.Skeleton.Data.Animations.Items[0].Name;
        }
        
        Debug.LogWarning($"В скелете {skeleton.name} нет анимаций!");
        return null;
    }

    // --- Внутренняя логика запуска (та же, что была раньше) ---
    private void PlayInternal(SkeletonAnimation targetSkeleton, string animName, bool returnToCombat)
    {
        if (targetSkeleton == null || string.IsNullOrEmpty(animName)) return;

        if (skeletonCombat != null) skeletonCombat.gameObject.SetActive(false);
        targetSkeleton.gameObject.SetActive(true);
        
        TrackEntry track = targetSkeleton.AnimationState.SetAnimation(0, animName, !returnToCombat);

        if (returnToCombat)
        {
            track.Complete += OnAnimationComplete;
        }
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        trackEntry.Complete -= OnAnimationComplete;
        ShowCombat();
    }

    private void SetActiveSkeletons(SkeletonAnimation activeSkeleton)
    {
        if (skeletonCombat != null) skeletonCombat.gameObject.SetActive(activeSkeleton == skeletonCombat);
        if (skeletonAttack != null) skeletonAttack.gameObject.SetActive(activeSkeleton == skeletonAttack);
        
        if (activeSkeleton == skeletonCombat && skeletonFX != null)
        {
            skeletonFX.gameObject.SetActive(false);
            skeletonFX.AnimationState.ClearTracks(); 
        }
    }
}