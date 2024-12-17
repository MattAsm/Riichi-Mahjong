using UnityEngine;

public enum EasyAIState{ Checking, Turn, Win}

public class EasyAI : MonoBehaviour
{

    private EasyAIState currentState;


    void Update()
    {

        switch (currentState)
        {
            case EasyAIState.Checking:
                Checking();
                break;
            case EasyAIState.Turn:
                Turn();
                break;
            case EasyAIState.Win:
                Win();
                break;
        }

        void Checking()
        {

        }

        void Turn()
        {

        }

        void Win()
        {

        }
    }
}
