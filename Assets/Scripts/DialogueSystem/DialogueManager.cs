using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image npcImage;
    public TextMeshProUGUI dialogueText;
    public Animator anim;

    private Queue<string> sentences;

    private Dictionary<int, List<string>> dialogueDict;

    private QuestSystem taskManager;


    void Start()
    {
        sentences = new Queue<string>();
        dialogueDict = new();
        taskManager = FindObjectOfType<QuestSystem>();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        dialogueDict = new Dictionary<int, List<string>>
        {
            {1, new List<string>
                {
                    "Governor: 'Our renewable energy output is lagging behind other cities.'",
                    "Governor: 'I need you to build 2 solar panels to help meet our sustainability goals.'"
                }
            },
            {2, new List<string>
                {
                    "Local Business Owner: 'Our energy bills are out of control.'",
                    "Local Business Owner: 'If you could upgrade a few of our buildings to be more energy efficient, we’d all save a fortune.'"
                }
            },
            {3, new List<string>
                {
                    "City Council Member: 'Our citizens are concerned about the pollution from the coal plant.'",
                    "City Council Member: 'It’s time to switch to a renewable energy source. Can you convert the coal plant?'"
                }
            },
            {4, new List<string>
                {
                    "Weather Forecaster: 'There’s a massive storm heading our way.'",
                    "Weather Forecaster: 'We need to increase battery storage by 20% to ensure the city stays powered.'"
                }
            },
            {5, new List<string>
                {
                    "Scientist: 'We’ve made a breakthrough in clean energy research!'",
                    "Scientist: 'But we need a Tech Lab to proceed with our studies. Can you build one for us?'"
                }
            },
            {6, new List<string>
                {
                    "Concerned Citizen: 'The pollution in our city is getting worse.'",
                    "Concerned Citizen: 'Please, upgrade at least 3 factories to cleaner energy. Our health depends on it.'"
                }
            },
            {7, new List<string>
                {
                    "Mayor of Neighboring City: 'We’ve had a power shortage in our city. We need help!'",
                    "Mayor of Neighboring City: 'Could you build a wind farm and supply us with energy?'"
                }
            },
            {8, new List<string>
                {
                    "Energy Advisor: 'The recent drought has severely impacted our hydropower output.'",
                    "Energy Advisor: 'We need to compensate by boosting solar or wind energy production.'"
                }
            },
            {9, new List<string>
                {
                    "Industrial Complex Manager: 'Our factories are underperforming due to outdated energy systems.'",
                    "Industrial Complex Manager: 'If you upgrade 2 power plants, we can improve efficiency.'"
                }
            },
            {10, new List<string>
                {
                    "Housing Official: 'The city’s population is growing rapidly. We need more housing!'",
                    "Housing Official: 'Build 5 energy-efficient homes to accommodate the influx of citizens.'"
                }
            },
            {11, new List<string>
                {
                    "Researcher: 'We’re on the verge of a breakthrough in renewable energy tech, but we need more funding.'",
                    "Researcher: 'Can you build an Innovation Center to help boost our research efforts?'"
                }
            },
            {12, new List<string>
                {
                    "Tech Company Representative: 'We’ve developed a new kind of battery, and we want to test it in your city.'",
                    "Tech Company Representative: 'Can you install 3 of them around the city for a pilot program?'"
                }
            },
            {13, new List<string>
                {
                    "Energy Regulator: 'New laws require that at least 30% of the city’s power comes from renewable sources.'",
                    "Energy Regulator: 'You need to adjust your energy mix to comply with this regulation.'"
                }
            },
            {14, new List<string>
                {
                    "Activist Leader: 'Nuclear energy is too dangerous! It’s time to decommission the reactor.'",
                    "Activist Leader: 'We demand that you replace it with clean, renewable energy options.'"
                }
            },
            {15, new List<string>
                {
                    "Meteorologist: 'A massive storm is about to hit the coast. Your Hydro Dam is at risk.'",
                    "Meteorologist: 'You should upgrade it to withstand the impact and continue supplying power.'"
                }
            },
            {16, new List<string>
                {
                    "Investor: 'I’m looking to fund energy research in your city.'",
                    "Investor: 'If you build two new research labs, I’ll provide the financial backing for advanced energy solutions.'"
                }
            },
            {17, new List<string>
                {
                    "Neighboring City Mayor: 'Our city is facing rolling blackouts. Can you help?'",
                    "Neighboring City Mayor: 'We need a 20% boost in energy production to keep the lights on.'"
                }
            },
            {18, new List<string>
                {
                    "Developer: 'I want to build a new apartment complex in your city, but it needs to be energy-efficient.'",
                    "Developer: 'Upgrade the city’s grid to ensure the complex uses smart energy solutions.'"
                }
            },
            {19, new List<string>
                {
                    "Citizen Group Leader: 'Electricity costs are too high, and people are struggling to pay their bills.'",
                    "Citizen Group Leader: 'You need to reduce energy prices by improving efficiency across the city.'"
                }
            },
            {20, new List<string>
                {
                    "Foreign Government Official: 'We’d like to partner with your city on a renewable energy project.'",
                    "Foreign Government Official: 'If you build an offshore wind farm, we’ll offer you favorable trade deals.'"
                }
            }
        };
    }


    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        npcImage.sprite = dialogue.image;
        sentences.Clear();

        List<int> keys = new List<int>(dialogueDict.Keys);
        int dialogueKey = -1;
        while (dialogueKey == -1 || EventManager.Instance.isCompleted(dialogueKey))
        {
            dialogueKey = keys[Random.Range(0, keys.Count)];

        }

        bool taskAssigned = taskManager.AssignTaskForDialogue(dialogueKey);
        if (taskAssigned)
        {


            List<string> randSent = dialogueDict[dialogueKey];

            dialogueDict.Remove(dialogueKey);
            foreach (string sentence in randSent)
            {
                sentences.Enqueue(sentence);
            }

            EventManager.Instance.StartEvent(dialogueKey);
            DisplayNextSentence();
        }
        else
        {
            anim.SetBool("isOpen", false);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        anim.SetBool("isOpen", false);
    }
}
