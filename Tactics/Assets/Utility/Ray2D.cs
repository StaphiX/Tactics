using UnityEngine;
using System.Collections;

// If we ever need a 2D ray / circle algorithm, I have a 3D one I wrote which we could rework
public class Ray2D 
{
	private Vector2 origin;
	private Vector2 direction;
	
	public Vector2 Origin 	 { get { return origin; } 	 set { origin =    value; } }
	public Vector2 Direction { get { return direction; } set { direction = value; } }
	
	public Ray2D(Vector2 origin, Vector2 direction)
	{
		this.origin = origin;
		//normalise
		direction /= direction.magnitude;
	}
	
	public bool Intersects(AABB2D bb, out float intersectionDistance)
	{
		//doesn't get set unless we return true, so set a dumb value
		intersectionDistance = float.MaxValue; 
		
		Vector2 dirfrac = new Vector2(0, 0);
		Vector2 bbMin	= bb.Min;
		Vector2 bbMax	= bb.Max;
	
		// we don't want to divide by zero
		dirfrac.x = direction.x != 0 ? 1.0f / direction.x : 0;
		dirfrac.y = direction.y != 0 ? 1.0f / direction.y : 0;
		//dirfrac.z = direction.z != 0 ? 1.0f / direction.z : 0;
	
		//min and max are the negative and positive corners of the bounding box
		float t1 = (bbMin.x - origin.x) * dirfrac.x;
		float t2 = (bbMax.x - origin.x) * dirfrac.x;
		float t3 = (bbMin.y - origin.y) * dirfrac.y;
		float t4 = (bbMax.y - origin.y) * dirfrac.y;
		//float t5 = (boundingBox.Min().z - origin.z) * dirfrac.z;
		//float t6 = (boundingBox.Max().z - origin.z) * dirfrac.z;
	
		float tmin = Mathf.Max( Mathf.Min(t1, t2), Mathf.Min(t3, t4) );
		
//		float tmin = max( 
//			max( min(t1, t2), min(t3, t4) ), 
//			min(t5, t6) );
		
		float tmax = Mathf.Min( Mathf.Max(t1, t2), Mathf.Max(t3, t4) );
		
//		float tmax = min( 
//			min( max(t1, t2), max(t3, t4) ), 
//			max(t5, t6) );
	
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
}