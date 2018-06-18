﻿using Hevadea.Registry;
using Hevadea.GameObjects.Entities;
using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class EntityStorage
    {
        public string Type { get; set; }
        public int Ueid { get; set; } = -1;

        public Dictionary<string, object> Data { get; set; }

        public EntityStorage(string type)
        {
            Type = type;
            Data = new Dictionary<string, object>();
        }

        public EntityStorage(string type, int ueid) : this (type)
        {
            Ueid = ueid;
        }

        public EntityStorage(string type, Dictionary<string, object> data)
        {
            Type = type;
            Data = data;
        }

        internal T ValueOf<T>(string name, T defaultValue)
        {
            return (T)(Data.ContainsKey(name) ? Data[name] : defaultValue);
        }

        public void Value<T>(string name, T value)
        {
            Data[name] = value;
        }

        public Entity ConstructEntity()
        {
            Entity entity = ENTITIES.Construct(Type);
            entity.Load(this);

            return entity;
        }
    }
}