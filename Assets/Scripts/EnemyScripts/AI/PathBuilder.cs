using Pathfinding;
using UnityEngine;

namespace EnemyScripts.AI
{
    public class PathBuilder
    {
        private readonly Seeker _seeker;
        public PathBuilder(Seeker seeker)
        {
            _seeker = seeker;
        }

        public Path Build(Vector3 from, Vector3 to)
        {
            return _seeker.StartPath(from, to);
        }
        
        /*private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
            }
        }*/
    }
}