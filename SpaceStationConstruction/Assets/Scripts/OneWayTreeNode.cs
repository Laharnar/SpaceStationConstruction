using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneWayTreeNode {
    public List<OneWayTreeNode> children = new List<OneWayTreeNode>();
    public QuestInfo data;// this should be serializable
    public QuestInfo questData { get { return data as QuestInfo; } }

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
        }
        // put down directions and mark those who have any incoming.
        bool[] hasInc = new bool[data.Length];
        for (int i = 0; i < data.Length; i++) {
            for (int j = 0; j < directions.nodes[i].directions.Length; j++) {
                if (nodes[i] == nodes[directions.nodes[i].directions[j]])
                    continue;
                nodes[i].Add(nodes[directions.nodes[i].directions[j]]);
                hasInc[directions.nodes[i].directions[j]] = true;
            }
        }
        // find root - node that has 0 incoming connections
        for (int i = 0; i < hasInc.Length; i++) {
            if (!hasInc[i]) {
                return nodes[i];
            }
        }
        return null;// err
    }
}
