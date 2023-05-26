using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

[RequireComponent(typeof(Entity))]
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image prefab;
    [SerializeField] private Vector2 offset = new Vector2(0, 1f);
    private Image _healthBarImage;
    private Image _fillImage;
    private Entity _tetherEntity;

    private static GameObject parnent;
    private Camera _camera;

    private void Awake()
    {
        if (!parnent) parnent = GameObject.Find("HealthDisplays");
        
        _healthBarImage = Instantiate(prefab, parnent.transform);
        _fillImage = _healthBarImage.transform.GetChild(0).GetComponent<Image>();
        
        _tetherEntity = GetComponent<Entity>();
        _tetherEntity.OnCurrentHealthChanged += UpdateBar;
        _camera = Camera.main;
    }

    private void UpdateBar()
    {
        float value = _tetherEntity.CurrentHealth / _tetherEntity.maxHealth;
        _fillImage.fillAmount = value;
    }

    private void OnEnable()
    {
        _healthBarImage.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        try
        {
            _healthBarImage.gameObject.SetActive(false);
        }
        catch
        {
            //ignore
        }
    }

    private void Update()
    {
        _healthBarImage.transform.position = _camera.WorldToScreenPoint(_tetherEntity.transform.position) + (Vector3)offset;
    }
}
