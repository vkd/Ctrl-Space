using System.Collections.Generic;
using Ctrl_Space.Gameplay.Bullets;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Gameplay
{
    class GameObjects
    {
        private int _objectId = 0;
        private List<GameObject> _gameObjects = new List<GameObject>();
        private Pool<Ship> _ships = new Pool<Ship>();
        private Pool<EnemyShip> _enemyShips = new Pool<EnemyShip>();
        private Pool<Asteroid> _asteroids = new Pool<Asteroid>();
        private Pool<SpeedBonus> _speedBonuses = new Pool<SpeedBonus>();
        private Pool<Medkit> _medkits = new Pool<Medkit>();
        private Pool<PlasmaBullet> _plasmaBullets = new Pool<PlasmaBullet>();
        private Pool<SinPlasmaBullet> _sinPlasmaBullets = new Pool<SinPlasmaBullet>();
        private Pool<Rocket> _rockets = new Pool<Rocket>();

        public GameObject GetObject(int id)
        {
            return _gameObjects[id];
        }

        public Ship CreateShip(Vector2 position, World world)
        {
            Ship ship = _ships.GetObject();
            ship.Reset(position, world);
            AddObject(ship);
            return ship;
        }

        public void ReleaseShip(Ship ship)
        {
            _ships.PutObject(ship);
        }

        public EnemyShip CreateEnemyShip(Vector2 position, World world, Ship target)
        {
            EnemyShip enemyShip = _enemyShips.GetObject();
            enemyShip.Reset(position, world, target);
            AddObject(enemyShip);
            return enemyShip;
        }

        public void ReleaseEnemyShip(EnemyShip enemyShip)
        {
            _enemyShips.PutObject(enemyShip);
        }

        public Asteroid CreateAsteroid()
        {
            Asteroid asteroid = _asteroids.GetObject();
            //asteroid.Reset();
            AddObject(asteroid);
            return asteroid;
        }

        public void ReleaseAsteroid(Asteroid asteroid)
        {
            _asteroids.PutObject(asteroid);
        }

        public SpeedBonus CreateSpeedBonus(Vector2 position)
        {
            SpeedBonus speedBonus = _speedBonuses.GetObject();
            speedBonus.Reset(position);
            AddObject(speedBonus);
            return speedBonus;
        }

        public void ReleaseSpeedBonus(SpeedBonus speedBonus)
        {
            _speedBonuses.PutObject(speedBonus);
        }

        public Medkit CreateMedkit(Vector2 position)
        {
            Medkit medkit = _medkits.GetObject();
            medkit.Reset(position);
            AddObject(medkit);
            return medkit;
        }

        public void ReleaseMedkit(Medkit medkit)
        {
            _medkits.PutObject(medkit);
        }

        public PlasmaBullet CreatePlasmaBullet()
        {
            PlasmaBullet plasmaBullet = _plasmaBullets.GetObject();
            plasmaBullet.Reset();
            AddObject(plasmaBullet);
            return plasmaBullet;
        }

        public void ReleasePlasmaBullet(PlasmaBullet plasmaBullet)
        {
            _plasmaBullets.PutObject(plasmaBullet);
        }

        public SinPlasmaBullet CreateSinPlasmaBullet()
        {
            SinPlasmaBullet plasmaBullet = _sinPlasmaBullets.GetObject();
            plasmaBullet.Reset();
            AddObject(plasmaBullet);
            return plasmaBullet;
        }

        public void ReleaseSinPlasmaBullet(SinPlasmaBullet plasmaBullet)
        {
            _sinPlasmaBullets.PutObject(plasmaBullet);
        }

        public Rocket CreateRocket(Vector2 position, Vector2 speed, float rotation)
        {
            Rocket rocket = _rockets.GetObject();
            rocket.Reset(position, speed, rotation);
            AddObject(rocket);
            return rocket;
        }

        public void ReleaseRocket(Rocket rocket)
        {
            _rockets.PutObject(rocket);
        }

        public void ReleaseObject(GameObject gameObject)
        {
            var type = gameObject.GetType();
            if (type == typeof(Ship))
                ReleaseShip((Ship)gameObject);
            else if (type == typeof(EnemyShip))
                ReleaseEnemyShip((EnemyShip)gameObject);
            else if (type == typeof(Asteroid))
                ReleaseAsteroid((Asteroid)gameObject);
            else if (type == typeof(SpeedBonus))
                ReleaseSpeedBonus((SpeedBonus)gameObject);
            else if (type == typeof(Medkit))
                ReleaseMedkit((Medkit)gameObject);
            else if (type == typeof(PlasmaBullet))
                ReleasePlasmaBullet((PlasmaBullet)gameObject);
            else if (type == typeof(SinPlasmaBullet))
                ReleaseSinPlasmaBullet((SinPlasmaBullet)gameObject);
            else if (type == typeof(Rocket))
                ReleaseRocket((Rocket)gameObject);
        }

        private void AddObject(GameObject gameObject)
        {
            if (gameObject.Id == -1)
            {
                _gameObjects.Add(gameObject);
                gameObject.Id = _objectId;
                _objectId++;
            }
        }
    }
}
