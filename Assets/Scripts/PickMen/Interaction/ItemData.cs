using Shears;
using UnityEngine;

namespace PickMen.Interaction
{
    [CreateAssetMenu(menuName = "PickMen/Item Data")]
    public partial class ItemData : ScriptableObject
    {
        [SerializeField]
        [AutoProperty("Name")]
        private string itemName;

        [SerializeField]
        [AutoProperty]
        private Sprite sprite;
    }
}
