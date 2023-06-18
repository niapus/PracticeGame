using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace Elementaria
{
    public static class MapCreator
    {
        private static readonly Dictionary<string, Func<ICreature>> dictionaryElements = new Dictionary<string, Func<ICreature>>();

        public static ICreature[,] CreateMap(string elements, string splitter = "\r\n")
        {
            var objects = elements.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
            var map = new ICreature[objects[0].Length, objects.Length];
            for (var x = 0; x < objects[0].Length; x++)
                for (var y = 0; y < objects.Length; y++)
                    map[x, y] = CreateCreatureBySymbol(objects[y][x]);
            return map;
        }
        
        private static ICreature CreateObjectByName(string nameElement)
        {
            if (!dictionaryElements.ContainsKey(nameElement))
            {
                var typeElement = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(z => z.Name == nameElement);
                if (typeElement == null)
                    throw new Exception($"Can't find type '{nameElement}'");
                dictionaryElements[nameElement] = () => (ICreature)Activator.CreateInstance(typeElement);
            }
            return dictionaryElements[nameElement]();
        }

        private static ICreature CreateCreatureBySymbol(char c)
        {
            switch (c)
            {
                case 'P':
                    return CreateObjectByName("Player");
                case 'S':
                    return CreateObjectByName("Stone");
                case 'B':
                    return CreateObjectByName("Box");
                case 'C':
                    return CreateObjectByName("Chest");
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for Create");
            }
        }
    }
}