using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.Maps;

public class VectorMap
{
    private VectorMap(LinkedList<Vector> vectors)
    {
        Vectors = vectors;
    }

    public IEnumerable<Vector> Vectors { get; private set; }

    public static VectorMap FromMatrixMap(MatrixMap matrixMap)
    {
        Vector currentVector = Vector.CreateStartingVector(matrixMap.StartingField);

        LinkedList<Vector> vectors = new LinkedList<Vector>();

        vectors.AddLast(currentVector);

        while (!currentVector.IsEndVector)
        {
            currentVector = currentVector.CreateAppendedVector();

            vectors.AddLast(currentVector);
        }

        return new VectorMap(vectors);
    }
}
