using System;
using Stores;
using TMPro;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Results
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI[] texts;

        [Inject]
        void Initialize(Score score)
        {
            var counts = score.ServedSanmas.Select(x =>
                {
                    if (x < 0.3f) return 0;
                    if (x < 0.7f) return 1;
                    return 2;
                })
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            foreach (var t in texts.Select((text, i) => (text, i)))
            {
                int count = 0;
                counts.TryGetValue(t.i, out count);
                t.text.text = count.ToString();
            }
        }
    }
}
