using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneWayTreeNode {
    public List<OneWayTreeNode> children = new List<OneWayTreeNode>();
    public QuestInfo data;// this should be serializable
    
    // local id, based from certain root.
    private int id;

    public QuestInfo questData { get { return data as QuestInfo; } }

    public OneWayTreeNode[] rAsList() {
        List<OneWayTreeNode> arr = new List<OneWayTreeNode>();
        Queue<OneWayTreeNode> q = new Queue<OneWayTreeNode>();
        q.Enqueue(this);
        while (q.Count > 0) {
            OneWayTreeNode node = q.Dequeue();
            arr.Add(node);
            for (int i = 0; i < node.children.Count; i++) {
                q.Enqueue(node.children[i]);
            }
        }
        return arr.ToArray();
    }

    public OneWayTreeNode Add(params OneWayTreeNode[] child) {
        children.AddRange(child);
        return this;
    }

    public void ExecuteTree(Action<OneWayTreeNode> evt) {
        evt(this);
        for (int i = 0; i < children.Count; i++) {
            children[i].ExecuteTree(evt);
        }
    }

    public static OneWayTreeNode ConstructQuestTree(object[] data, QuestTreeDirections directions) {
        if (data.Length == 0 || directions.nodes.Length != data.Length) {
            Debug.Log("err: "+(data.Length == 0) + " "+ (directions.nodes.Length != data.Length));
            return null;
        }
        OneWayTreeNode[] nodes = new OneWayTreeNode[data.Length];
        for (int i = 0; i < data.Length; i++) {
            nodes[i] = new OneWayTreeNode();
            nodes[i].data = data[i] as QuestInfo ;
            nodes[i].id = i;
        }
        // put down directions and mark those who have any incoming.
        // to find root later(0 incoming).
        bool[] hasInc = new bool[data.Length];
        for (int i = 0; i < data.Length; i++) {
            for (int j = 0; j < directions.nodes[i].directions.Length; j++) {
                if (nodes[i] == nodes[directions.nodes[i].directions[j]])
                    continue;
                nodes[i].Add(nodes[directions.nodes[i].directions[j]]);
                hasInc[directions.nodes[i].directions[j]] = true;
            }
        }
        // paths.. just map who the child is. or connect them in linear fashion
        // 
        // find root - node that has 0 incoming connections
        for (int i = 0; i < hasInc.Length; i++) {
            if (!hasInc[i]) {
                return nodes[i];
            }
        }
        return null;// err
    }

    internal QuestInfo[] GetQuestData() {
        throw new NotImplementedException();
    }
}
