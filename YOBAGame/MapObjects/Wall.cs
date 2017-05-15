using System;
using System.Collections.Generic;
using System.Linq;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using YOBAGame.Extensions;
using YOBAGame.GameRules;

namespace YOBAGame.MapObjects
{
    public class Wall : AbstractStaticPhysicalObject, IDrawableObject
    {
        public Wall(Vector2 coordinates, IShape hitBox, IGameRules rules) : base(coordinates, hitBox.ToPolygon2(), rules)
        {
        }

        public override IEnumerable<IMapObject> GeneratedObjects()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override bool ShouldBeDeleted
        {
            get { return false; }
            set { }
        }


        public override IEnumerable<IMapObject> DeleteResult()
        {
            return Enumerable.Empty<IMapObject>();
        }

        public override void Decide(double dt, GameState gameState)
        {
        }

        public void PushOut(IPhysicalObject obj)
        {
            var wall = this;
            if (!(wall.HitBox is Polygon2) || !(obj.HitBox is Circle2))
                throw new NotImplementedException("Only Polygon2 with Circle2 collisions can be resolved.");

            var polygon = (Polygon2) wall.HitBox;
            var circle = (Circle2) obj.HitBox;
            var O = obj.Coordinates - wall.Coordinates;
            foreach (var threeGramm in Enumerable
                .Range(0, polygon.VerticesCount)
                .Select(i => polygon[i])
                .CyclicThreeGramms())
            {
                var A = threeGramm[0];
                var B = threeGramm[1];
                var C = threeGramm[2];
                var AB = B - A;
                var AO = O - A;
                if (AB.PseudoVectMul(AO) < double.Epsilon)
                    continue;
                var BO = O - B;
                var P1 = AB.GetOrthogonalVector(Direction.LEFT).Normalize();
                // circle with segment collision case
                if (P1.PseudoVectMul(AO) * P1.PseudoVectMul(BO) < double.Epsilon)
                {
                    obj.Coordinates += P1 * P1.DotProduct(AO);
                    obj.Speed -= P1 * P1.DotProduct(obj.Speed);
                    return;
                }
                var BC = C - B;
                if (BC.PseudoVectMul(AO) < double.Epsilon)
                    continue;
                var P2 = BC.GetOrthogonalVector(Direction.LEFT);

                // circle with corner collision case
                if (P1.PseudoVectMul(AO) * P2.PseudoVectMul(BO) < double.Epsilon)
                {
                    var AO_l = AO.Length;
                    obj.Coordinates += AO * ((circle.Radius - AO_l) / AO_l);
                    var N = AO.Normalize();
                    obj.Speed -= N * N.DotProduct(obj.Speed);
                    return;
                }
            }
        }
    }
}