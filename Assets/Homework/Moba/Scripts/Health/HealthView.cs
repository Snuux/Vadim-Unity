using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] AgentCharacter _agentCharacter;

    [SerializeField] Transform _followTarget;
    [SerializeField] Vector3 _followOffset;
    [SerializeField] Slider _healthSlider;

    private void Update()
    {
        UpdateHealthBar(_agentCharacter.CurrentHealth, _agentCharacter.MaxHealth);

        FollowTarget();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth / maxHealth;
    }

    private void FollowTarget()
    {
        transform.position = _followTarget.position + _followOffset;
        transform.rotation = Camera.main.transform.rotation;
    }
}