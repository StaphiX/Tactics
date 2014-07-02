using UnityEngine;
using System.Collections;

public class AABB2D {
	
	private Vector2 min;
	private Vector2 max;
		
	public Vector2 Min { get { return min; } set { min = value; } }
	public Vector2 Max { get { return max; } set { max = value; } }
	
	public AABB2D( Vector2 min, Vector2 max )
	{
		this.min = min;
		this.max = max;
	}
	
	bool Intersects( AABB2D bb )
	{
		Vector2 bbMin = bb.Min;
		Vector2 bbMax = bb.Max;
		
		return (
		( min.x < bbMax.x && 
		  min.y < bbMax.y ) 
		 && 
		( max.x > bbMin.x && 
		  max.y > bbMin.y ));
	}
	
	public bool Intersects( Ray2D ray, out float intersectionDistance )
	{
		//doesn't get set unless we return true, so set a dumb value
		intersectionDistance = float.MaxValue; 
		
		Vector2 dirfrac = new Vector2(0, 0);
		Vector2 rayDir 	= ray.Direction;
		Vector2 rayOrig = ray.Origin;
		
		// we don't want to divide by zero
		dirfrac.x = rayDir.x != 0 ? 1.0f / rayDir.x : 0;
		dirfrac.y = rayDir.y != 0 ? 1.0f / rayDir.y : 0;
	
		//min and max are the negative and positive corners of the bounding box
		float t1 = (min.x - rayOrig.x) * dirfrac.x;
		float t2 = (max.x - rayOrig.x) * dirfrac.x;
		float t3 = (min.y - rayOrig.y) * dirfrac.y;
		float t4 = (max.y - rayOrig.y) * dirfrac.y;
	
		float tmin = Mathf.Max( Mathf.Min(t1, t2), Mathf.Min(t3, t4) );	
		float tmax = Mathf.Min( Mathf.Max(t1, t2), Mathf.Max(t3, t4) );

		// if tmax < 0, ray is geometrically intersecting AABB, but 
		// its origin is ahead of the box so it actually doesn't.
		if (tmax < 0)
			return false;
	
		// if tmin > tmax, ray doesn't intersect AABB
		if (tmin > tmax)
			return false;
	
		// origin is inside the box
		if (tmin < 0) 
		{ 
			intersectionDistance = 0;
			return true; 
		}
		
		intersectionDistance = tmin;
		return true; 
	}
	
	bool Intersects(Vector2 point)
	{
		float pX = point.x;
		float pY = point.y;
		
		if ((pX > min.y && pY > min.y) && (pX < max.y && pY < max.y))
			return true;
	
		return false;
	}
}