using Godot;
using System.Collections;

public class Matrix<T> : IEnumerable {

    public readonly int xLenght;
    public readonly int yLenght;
    public readonly Vector2 offset;

    private T[,] buffer;

    public Matrix(Vector2 bottomRight){
        this.xLenght = (int)bottomRight.x;
        this.yLenght = (int)bottomRight.y;
        this.offset = Vector2.Zero;
        Initialize();
    }

    public Matrix(Vector2 bottomRight, Vector2 upperLeft){
        this.xLenght = (int)(bottomRight.x - upperLeft.x);
        this.yLenght = (int)(bottomRight.y - upperLeft.y);
        this.offset = upperLeft;
        Initialize();
    }

    public Matrix(Rect2 rec){
        Vector2 bottomRight = rec.End - Vector2.One;
        Vector2 upperLeft = rec.Position;
        this.xLenght = (int)(bottomRight.x - upperLeft.x);
        this.yLenght = (int)(bottomRight.y - upperLeft.y);
        this.offset = upperLeft;
        Initialize();
    }

    private void Initialize(){
        buffer = new T[yLenght, xLenght];
    }

    public Matrix<T> SetRelative(int x, int y, int xRelative, int yRelative, T value){
        buffer[y-(int)offset.y + yRelative, x-(int)offset.x + xRelative] = value;
        return this; 
    }

    public T GetRelative(int x, int y, int xRelative, int yRelative){
        return buffer[y-(int)offset.y + yRelative, x-(int)offset.x + xRelative];
    }

    public T this[int x, int y]{
        get => Get(x,y);
        set => Set(x, y, value);
    }

    public T this[Vector2 v]{
        get => Get(v);
        set => Set(v, value);
    }

    public T Get(int x, int y){
        return GetRelative(x, y, 0, 0);
    }

    public T Get(Vector2 v){
        return GetRelative(v, Vector2.Zero);
    }

    public Matrix<T> Set(int x, int y, T value){
        return SetRelative(x, y, 0, 0, value);
    }

    public Matrix<T> Set(Vector2 v, T value){
        return SetRelative(v, Vector2.Zero, value);
    }

    public Matrix<T> SetRelative(Vector2 v, Vector2 relative, T value){
        return SetRelative((int)v.x, (int)v.y, (int)relative.x, (int)relative.y, value);
    }

    public T GetRelative(Vector2 v, Vector2 relative){
        return GetRelative((int)v.x, (int)v.y, (int)relative.x, (int)relative.y);
    }

    public IEnumerator GetEnumerator(){
        for(int y=0; y<yLenght;y++){
            for(int x=0; x<xLenght;x++){
                yield return buffer[y,x];
            }
        }
    }
}