using System.Collections;
using System.Collections.Generic;
using TMPro;
using TrapLight.Sound;
using TrapLight.UI;
using UnityEngine;
namespace TrapLight.Light
{

    public class BlackParticle : MonoBehaviour
    {
        [SerializeField] private float speed = 50;
        private Rigidbody2D rb2D;

        private LineRenderer line; // Reference to LineRenderer
        private Vector3 startPos;    // Start position of line
        private Vector3 endPos;    // End position of line
        [SerializeField] private GameObject explosiveItemPrefab;
        [SerializeField] private int explosiveItemCount = 3;
        [SerializeField] private int health = 100;
        [SerializeField] private int MAX_HEALTH = 100;
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private TextMeshProUGUI bombText;
        [SerializeField] private int level = 1;
        [SerializeField] private int EXPLOSIVE_COUNT = 3;
        private List<GameObject> wallLines;
        private List<GameObject> wallColliders;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            RefreshHealthText();
        }
        private void Update()
        {
            rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed);

            HandleInput();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.CompareTag(GlobalConstant.LIGHT_TAG))
            {
                Debug.Log(collision.gameObject.tag);
                if (AddHealth(-1) <= 0)
                {
                    UIController.Instance.SetGameOverUI(true);
                }
            }
        }

        void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (line == null)
                    CreateLine();

                line.SetPosition(0, rb2D.position);
                startPos = rb2D.position;
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                if (line)
                {

                    line.SetPosition(1, rb2D.position);
                    endPos = rb2D.position;
                    AddColliderToLine();
                    line = null;
                }
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                if (line)
                {

                    line.SetPosition(1, rb2D.position);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                CreateExplosive();
            }
        }

        private void CreateLine()
        {

            line = new GameObject("Line").AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Diffuse"));
            line.useWorldSpace = true;

            if (wallLines == null) wallLines = new List<GameObject>();
            wallLines.Add(line.gameObject);
        }

        // Following method adds collider to created line
        private void AddColliderToLine()
        {
            BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
            col.transform.parent = line.transform; // Collider is added as child object of line
            float lineLength = Vector3.Distance(startPos, endPos); // length of line

            col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
            Vector3 midPoint = (startPos + endPos) / 2;
            col.transform.position = midPoint; // setting position of collider object
                                               // Following lines calculate the angle between startPos and endPos
            float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
            if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);

            if (col != null && !float.IsNaN(angle))
            {
                col.transform.Rotate(0, 0, angle);
                if (wallColliders == null) wallColliders = new List<GameObject>();
                wallColliders.Add(col.gameObject);
            }
        }

        private void CreateExplosive()
        {
            if (explosiveItemPrefab != null && explosiveItemCount > 0)
            {
                SoundManager.Instance.Play(SoundType.SetExplosive);
                Instantiate(explosiveItemPrefab, transform.position, Quaternion.identity);
                explosiveItemCount--;
                RefreshBombText();
            }
        }

        public int AddHealth(int val)
        {
            this.health += val;
            if (this.health <= 0)
            {
                this.health = 0;
            }
            if (this.health > MAX_HEALTH)
            {
                this.health = MAX_HEALTH;
            }
            RefreshHealthText();
            return health;
        }
        private void RefreshHealthText()
        {
            if (healthText != null)
                healthText.text = health.ToString();
        }
        private void RefreshBombText()
        {
            if (bombText != null)
                bombText.text = explosiveItemCount.ToString();
        }

        public void UpgradeLevel(int waveCount)
        {
            level = waveCount;
            AddHealth(MAX_HEALTH);
            explosiveItemCount = EXPLOSIVE_COUNT * level;
            RefreshBombText();
        }

        public void DeleteAllWalls()
        {
            if (wallLines != null)
                for (int i = 0; i < wallLines.Count; i++)
                {
                    Destroy(wallLines[i]);
                }

            if (wallColliders != null)
                for (int i = 0; i < wallColliders.Count; i++)
                {
                    Destroy(wallColliders[i]);
                }
        }

        public int GetExplosiveCount()
        {
            return explosiveItemCount;
        }
        public int GetMaxExplosiveCount()
        {
            return (EXPLOSIVE_COUNT * level);
        }
    }
}