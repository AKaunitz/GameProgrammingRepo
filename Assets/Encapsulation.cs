using UnityEngine;

public class Encapsulation : MonoBehaviour
{
    private string playerName = "Fox";
    private int maxHealth = 350;
    private int currentHealth = 350;



    public delegate void HealthChanged(int newHealth);
    public event HealthChanged OnHealthChanged;




    public string PlayerName
    {
        get { 
            return playerName; 
        }
        private set { 
            playerName = value; 
        }
    }




    public int CurrentHealth
    {
        get { 
            return currentHealth; 
        }
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth); // makes sure it is not float
            OnHealthChanged.Invoke(currentHealth);
        }
    }





    public int MaxHealth
    {
        get { 
            return maxHealth; 
        }
    }




    private void Start()
    {
        OnHealthChanged += DisplayHealth;

        Debug.Log($"A wild {PlayerName} appeared with {CurrentHealth}/{MaxHealth} HP!");

        TakeDamage(20);
        Heal(10);
    }




    public void TakeDamage(int damage)
    {
        Debug.Log($"Oh no! {PlayerName} took {damage} damage.");
        CurrentHealth -= damage;
    }






    public void Heal(int amount)
    {
        Debug.Log($"Yes, magic! {PlayerName} healed {amount} HP.");
        CurrentHealth += amount;
    }







    private void DisplayHealth(int newHealth)
    {
        Debug.Log($"Current HP: {newHealth}/{MaxHealth}");
    }





}