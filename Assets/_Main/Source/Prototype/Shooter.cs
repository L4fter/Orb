using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;
    
   // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2d = new Vector2(mousePosition.x, mousePosition.y);
        var pos2d = new Vector2(transform.position.x, transform.position.y);
        var direction = (mousePos2d - pos2d).normalized;
        Debug.DrawLine(pos2d, pos2d + direction, Color.red);
        
        if (Input.GetMouseButton(0))
        {
            var distanceFromCenter = this.transform.localScale.x * 0.1f + 0.1f;
            var bullet = Instantiate(BulletPrefab, pos2d + direction * distanceFromCenter, Quaternion.identity);
            var gravitatingObject = bullet.GetComponent<GravitatingObject>();
            gravitatingObject.Velocity = this.GetComponent<GravitatingObject>().Velocity;
            gravitatingObject.StartAcceleration(direction*4, 0.2f);
        }    
    }
}
