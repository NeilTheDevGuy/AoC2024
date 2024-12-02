﻿namespace AoC2024.Utils;

public static class Extensions
{
    public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
    {
        for (var node = list.First; node != null; node = node.Next)
        {
            yield return node;
        }
    }
}
