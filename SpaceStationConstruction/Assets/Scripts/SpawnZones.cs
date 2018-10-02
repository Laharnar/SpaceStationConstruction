using UnityEngine;

//spawn zones are limited to a square around spaceship
public class SpawnZones :MonoBehaviour{

    public float width, length;

    public Vector2 GetRandomSpawnPosInSquareEdge(Vector2 center, Vector2 influence) {
        Vector2 dir = 
            (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized
            + influence
            )
            * Mathf.Sqrt(width*width + length*length)/2;
        //return dir+center;
        return NearestPointOnBoxPerimiter(width, length, center, center+dir);
    }

    Vector2 NearestPointOnBoxPerimiter(float wid, float len, Vector2 center, Vector2 pos) {
        float r = center.x + wid / 2;
        float l = center.x - wid / 2;
        float t = center.y + len / 2;
        float b = center.y - len / 2;

        pos = new Vector2(Mathf.Clamp(pos.x, l, r), Mathf.Clamp(pos.y, t, b));

        float dl = Mathf.Abs(pos.x - l), dr = Mathf.Abs(pos.x - r),
            dt = Mathf.Abs(pos.y - t), db = Mathf.Abs(pos.y - b);

        float min = Mathf.Min(dl, dr, dt, db);
        if (min == dt)
            return new Vector2(pos.x, t);
        if (min == db)
            return new Vector2(pos.x, b);
        if (min == dl)
            return new Vector2(l, pos.y);
        return new Vector2(r, pos.y);
    }
  /*  local abs, min, max = math.abs, math.min, math.max

local function clamp(x, lower, upper)
  return max(lower, min(upper, x))
end

local function getNearestPointInPerimeter(l, t, w, h, x, y)
  local r, b = l + w, t+h

    x, y = clamp(x, l, r), clamp(y, t, b)

  local dl, dr, dt, db = abs(x - l), abs(x - r), abs(y - t), abs(y - b)
  local m = min(dl, dr, dt, db)

  if m == dt then return x, t end
  if m == db then return x, b end
  if m == dl then return l, y end
  return r, y
end
*/

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, length, 0));
    }

}
