using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyInteractable : XRGrabInteractable {

    enum KeyPart { HEAD = 0, BODY, NONE = 99 }

    bool isGrabed;
    bool isCombined;
    [Header("Key Option")]
    [SerializeField]
    KeyPart partType;
    [SerializeField]
    GameObject combinedObj;

    protected override void Awake() {

        base.Awake();

        isGrabed = false;
        isCombined = false;

        selectEntered.AddListener((_arg) => { isGrabed = true; });
        selectExited.AddListener((_arg) => { isGrabed = false; });

    }


    public void Print(string _str) { 
    
        Debug.Log(_str);
        
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Key")) { 
        
            KeyInteractable other = collision.gameObject.GetComponent<KeyInteractable>();

            if (!isCombined && 
                (int)partType + (int)other.partType == 1 && 
                isGrabed && other.isGrabed) { 
            
                isCombined = true;
                other.isCombined = true;

                if (combinedObj != null) {

                    gameObject.SetActive(false);
                    other.gameObject.SetActive(false);

                    Instantiate(combinedObj, (transform.position + other.transform.position) / 2, Quaternion.identity);
                    

                }

            }

        }

    }


}
