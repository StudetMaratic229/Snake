using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    [SerializeField] private Transform[] tails;
    [SerializeField] private Button Left, Right;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private int a=0;
    private LinkedList<Transform> _tailesLinked;
    [SerializeField] private int intToSave;

    private void Start()
    {
        Help.PauseClicked += OnFreeze;
        Left.onClick.AddListener(() => StartCoroutine(RotateTail(-90))); 
        Right.onClick.AddListener(() => StartCoroutine(RotateTail(90)));
        _tailesLinked = new LinkedList<Transform>();
        _tailesLinked.AddLast(transform);
        Array.ForEach(tails, t => _tailesLinked.AddLast(t));
        intToSave = PlayerPrefs.GetInt("Saved", 0);
    }

    private void OnDestroy()
    {
        Help.PauseClicked -= OnFreeze;
    }

    private void Update()
    {
        Move();
        Rotate();
        string s1 = a.ToString();
        _score.text = s1;
        if (a > intToSave)
        {
            intToSave = a;
        }
    }
    

    private void OnFreeze(bool isFreeze)
    {
        Left.interactable = !isFreeze;
        Right.interactable = !isFreeze;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            PlayerPrefs.SetInt("Saved", intToSave);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Saveda", a);
            PlayerPrefs.Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Me.u");
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            LinkedListNode<Transform> last = _tailesLinked.Last;
            _tailesLinked.RemoveLast();
            LinkedListNode<Transform> body = _tailesLinked.Last;
            Transform newTransform = Instantiate(body.Value, body.Value.position-body.Value.forward, body.Value.rotation);
            newTransform.tag = "Finish";
            last.Value.position -= body.Value.forward;
            _tailesLinked.AddLast(newTransform);
            _tailesLinked.AddLast(last);
            float x = Random.Range(56.6f, 73.3f);
            float z = Random.Range(-4f, 15.2f); // Если игра в 2d, то z = 1f;
            other.gameObject.transform.position = new Vector3(x,1,z);
            a += 1;
            Debug.Log(a);
        }

    }

    private void Move()
    {
        LinkedListNode<Transform> node = _tailesLinked.First;
        while (node != null)
        {
            node.Value.position += node.Value.forward * Time.deltaTime * speed;
            node = node.Next;
        }
    }
    
    private void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
            StartCoroutine(RotateTail(-90));

        if (Input.GetKeyDown(KeyCode.D)) 
            StartCoroutine(RotateTail(90));
    }

    private IEnumerator RotateTail(int angle)
    {
        int idx = 1;
        LinkedListNode<Transform> node = _tailesLinked.First;
        node.Value.Rotate(0, angle, 0);

        node = node.Next;
        while (node != null)
        {
            yield return new WaitForSeconds(1f / speed);
            node.Value.Rotate(0, angle, 0);
            node = node.Next;
            idx += 1;
        }
    }
}