using UnityEngine;

public class RootGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfBranches;
    public int numberOfCubesPerBranch;
    public int branchingFrequency;
    public int maxBranchLevel;
    public float height;
    public float scaleFactor;
    public float deviation;
    public float growthSpeed = 1f;
    private int cubeIndex = 0;
    private int branchIndex = 0;
    private int branchLevel = 0;
    private Vector3[] directions;
    private Vector3[] previousPositions;
    private Vector3[] positions;
    private Quaternion[] rotations;
    private Vector3[] scales;
    private bool reverse = false;

    private void Start()
    {
        directions = new Vector3[numberOfBranches];
        previousPositions = new Vector3[numberOfBranches];
        positions = new Vector3[numberOfBranches];
        rotations = new Quaternion[numberOfBranches];
        scales = new Vector3[numberOfBranches];

        Vector3 startPoint = Vector3.zero;

        for (int b = 0; b < numberOfBranches; b++)
        {
            float angle = Random.Range(0, 360);
            float distance = Random.Range(0.1f, 1);
            directions[b] = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

            previousPositions[b] = startPoint;
        }
    }

    private void Update()
    {
        if (branchLevel >= maxBranchLevel)
        {
            return;
        }

        for (int i = 0; i < numberOfBranches; i++)
        {
            Vector3 position = previousPositions[i] - directions[i];
            position.y = height * cubeIndex / numberOfCubesPerBranch;

            float randomAngle = Random.Range(-deviation, deviation);
            directions[i] = Quaternion.Euler(0, randomAngle, 0) * directions[i];

            rotations[i] = Quaternion.LookRotation(position - previousPositions[i]);

            positions[i] = position;

            Vector3 scale = new Vector3(1, 1 - scaleFactor * cubeIndex / numberOfCubesPerBranch, 1);
            scales[i] = scale;
        }

        for (int i = 0; i < numberOfBranches; i++)
        {
            GameObject cube = Instantiate(cubePrefab, positions[i], rotations[i]);
            cube.transform.localScale = scales[i];
            cube.transform.parent = transform;
            previousPositions[i] = positions[i];
        }

        cubeIndex++;
        if (cubeIndex >= numberOfCubesPerBranch)
        {
            cubeIndex = 0;
            branchIndex++;
            if (branchIndex >= branchingFrequency)
            {
                branchIndex = 0;
                branchLevel++;
                if (branchLevel >= maxBranchLevel)
                {
                    return;
                }
            }
        }

        Time.timeScale = growthSpeed;
    }

}