using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour, IInteractable
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text nameText;
    
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] private string[] npcDialogues;
    [SerializeField] private string[] playerDialogues;
    [SerializeField] private string npcName;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip TalkingClip;
    private AudioSource audioSource;

    private string playerName = "Player";
    
    private bool hadConversation = false;
    private bool isTalking = false;
    
    public StarterAssetsInputs inputSource;
    
    private void Awake()
    {
        // safety: ensure audio source exists if clip provided
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;
            audioSource.loop = true;
        }
    }

    public void Interact()
    {
        // start the dialogue sequence
        if (isTalking) return;

        StartCoroutine(DisplayDialogue());
        player.SetActive(false);
        
    }
    
    private void StartTalkingSound()
    {
        if (TalkingClip != null && audioSource != null)
        {
            audioSource.clip = TalkingClip;
            audioSource.Play();
        }
    }
    private IEnumerator WaitForAdvance()
    {
        // wait until the configured key is pressed
        yield return new WaitUntil(() => inputSource != null && inputSource.jump);
        inputSource.jump = false;
    }
    
    private IEnumerator DisplayDialogue()
    {
        // show the dialogue panel
        isTalking = true;
        dialoguePanel.SetActive(true);
        
        if (hadConversation)
        {
            // if already had conversation, show a single line
            StartTalkingSound();
            nameText.text = npcName;
            dialogueText.text = "You should get you memory buffer checked, we've been through this already.";
            
            yield return WaitForAdvance();
            {
                ClearText();
                audioSource.Stop();
            }
            dialoguePanel.SetActive(false);
            player.SetActive(true);
            isTalking = false;
            yield break;
        }
        
        for (int i = 0; i < npcDialogues.Length; i++)
        {
            // NPC speaks
            nameText.text = npcName;
            dialogueText.text = npcDialogues[i];
            StartTalkingSound();
            yield return WaitForAdvance();
            {
                ClearText();
            }
            audioSource.Pause();
            yield return new WaitForSeconds(0.5f);

            if (i < playerDialogues.Length)
            {
                // Player responds
                nameText.text = playerName;
                dialogueText.text = playerDialogues[i];
                yield return WaitForAdvance();
                {
                    ClearText();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        audioSource.Stop();
        
        dialoguePanel.SetActive(false);
        player.SetActive(true);
        isTalking = false;
        
        hadConversation = true;

        if (GameState.Instance != null)
        {
            GameState.Instance.SetFlag($"HadConversationWith_{npcName}", true);
        }
        
    }
    
    private void ClearText()
    {
        nameText.text = "";
        dialogueText.text = "";
    }


}
