using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    private Vector2 mousePosition;
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 pointD;

    private Transform firstCollidedRectangle;
    private List<Transform> rectanglesInSelectionArea;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;

        boxCollider = GetComponent<BoxCollider2D>();

        rectanglesInSelectionArea = new List<Transform>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            pointA = GetMousePosition();
        } 
        if (Input.GetMouseButton(0)) {
            mousePosition = GetMousePosition();

            pointC = mousePosition;
            pointB = new Vector2(pointA.x, pointC.y);
            pointD = new Vector2(pointC.x, pointA.y);

            DrawSelectionArea();
        }

        if (Input.GetMouseButtonUp(0)) {
            PaintRectangles();
            ResetAllProperties();
        }
    }

    private void ResetAllProperties() {
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.SetPosition(2, Vector2.zero);
        lineRenderer.SetPosition(3, Vector2.zero);

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        firstCollidedRectangle = null;
        rectanglesInSelectionArea.Clear();
    }

    private void DrawSelectionArea() {
        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);
        lineRenderer.SetPosition(2, pointC);
        lineRenderer.SetPosition(3, pointD);

        ChangeColliderArea();
    }

    private void ChangeColliderArea() {
        float selectionAreaMinX = Mathf.Min(pointA.x, pointB.x, pointC.x, pointD.x);
        float selectionAreaMaxX = Mathf.Max(pointA.x, pointB.x, pointC.x, pointD.x);

        float selectionAreaMinY = Mathf.Min(pointA.y, pointB.y, pointC.y, pointD.y);
        float selectionAreaMaxY = Mathf.Max(pointA.y, pointB.y, pointC.y, pointD.y);

        boxCollider.size = new Vector2(selectionAreaMaxX - selectionAreaMinX, selectionAreaMaxY - selectionAreaMinY);

        boxCollider.offset = new Vector2((selectionAreaMaxX + selectionAreaMinX) / 2, (selectionAreaMaxY + selectionAreaMinY) / 2);
    }

    private void PaintRectangles() {
        foreach (Transform rectangle in rectanglesInSelectionArea) {
            rectangle.GetComponent<SpriteRenderer>().color = lineRenderer.endColor;
        }

        LevelManager.GetInstance().CheckLevelCompleted();
    }

    private Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (firstCollidedRectangle == null) {
            firstCollidedRectangle = collision.transform;

            Color firstRectanglesColor = firstCollidedRectangle.GetComponent<SpriteRenderer>().color;
            lineRenderer.startColor = firstRectanglesColor;
            lineRenderer.endColor = firstRectanglesColor;
        }

        if (!rectanglesInSelectionArea.Contains(collision.transform)) {
            rectanglesInSelectionArea.Add(collision.transform);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform == firstCollidedRectangle) {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            firstCollidedRectangle = null;
        }

        if (rectanglesInSelectionArea.Contains(collision.transform)) {
            rectanglesInSelectionArea.Remove(collision.transform);
        }
    }
}
