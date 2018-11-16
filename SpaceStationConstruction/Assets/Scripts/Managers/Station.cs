using System;
using UnityEngine;

[System.Serializable]
public class Station {
    [Header("TEMPORARY. missing multiple choices.")]
    // current version, assumes all station pieces are already 
    // placed and just need to be enabled.
    public Transform[] stationPieces;

    // for now, we build just by going from 1 piece to next
    // limit all code to this class.
    int activePiece = 0;

    public void Init() {
        for (int i = 0; i < stationPieces.Length; i++) {
            stationPieces[i].gameObject.SetActive(false);
        }

        BuildNextPiece(stationPieces[activePiece]);
    }

    public Vector3 NextBuildPos { get {
            if (activePiece+1 <stationPieces.Length) {
                return stationPieces[activePiece + 1].position;
            }
            Debug.Log("No avaliable build pos");
            return new Vector2(1000,0);
        }

    }

    public void BuildNextPiece(Transform piece) {
        if (piece) {
            piece.gameObject.SetActive(true);
        } else {
            Debug.Log("Piece is null. End of builds?");
        }
    }

    public Transform[] NextAvaliablePieces() {
        activePiece++;
        if (activePiece < stationPieces.Length) {
            return new Transform[1] { stationPieces[activePiece] };
        }
        return new Transform[0];
    }

    internal Transform NextAvaliablePiece() {
        Transform[] t = NextAvaliablePieces();
        if (t.Length > 0) {
            return t[0];
        }
        return null;
    }
}
