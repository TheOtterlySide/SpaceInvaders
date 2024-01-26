using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Bunker
{
    public class BunkerHitPoints : MonoBehaviour
    {
        private int life = 3;

        [SerializeField] 
        private SpriteLibrary lib;

        private SpriteLibraryAsset libAsset;      
        
        [SerializeField] 
        private SpriteResolver mySpriteResolver;
        // Start is called before the first frame update
        void Start()
        {
            libAsset = lib.spriteLibraryAsset;
            mySpriteResolver = gameObject.GetComponent<SpriteResolver>();
            LifeHandling();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void LifeHandling()
        {
            
            switch (life)
            {
                case 3:
                    mySpriteResolver.SetCategoryAndLabel("FULL HEALTH", gameObject.name);
                    mySpriteResolver.ResolveSpriteToSpriteRenderer();
                    break;
                case 2:
                    mySpriteResolver.SetCategoryAndLabel("HIT", gameObject.name);
                    mySpriteResolver.ResolveSpriteToSpriteRenderer();
                    break;
                case 1:
                    mySpriteResolver.SetCategoryAndLabel("NEARLY DEAD", gameObject.name);
                    mySpriteResolver.ResolveSpriteToSpriteRenderer();
                    break;
                case 0:
                    Dead();
                    break;
                default:
                    break;
                
            }
        }

        private void Dead()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Contains("PlayerBullet"))
            {
                //transform.parent.GetComponent<BunkerManager>().CustomTriggerEnter(gameObject);
                LifeHandling();
            }
        
        }
    }
}