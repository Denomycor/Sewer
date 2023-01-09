using Godot;
using System.Collections.Generic;

public class BinarySpacePertitionTest : Node2D
{
    [Export] int deviation = 1;
    Partition initPartition = new Partition(0,0,100,100);
    [Export] int minLength = 6;
    [Export] int minHeight = 4;
    [Export] int size = 10;

    [Export] float offset = 1.2f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        newTest(1);
    }

    private void newTest(ulong seed){
        //intialize the partition list
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Seed = seed;
        BinarySpacePartitioning.deviation = deviation;
        List<Partition> list = BinarySpacePartitioning.binarySpacePartition(initPartition,minLength,minHeight, rng);
        BinarySpacePartitioning.spreadPartitionsApart(list, offset);

        //vizualize partitions
        foreach(Partition partition in list){
            Panel panel = new Panel();
            panel.MarginRight = partition.length*size;
            panel.MarginBottom = partition.height*size;
            panel.RectPosition = new Vector2(partition.bottomLeftCornerX, partition.bottomLeftCornerY) * size ;
            panel.Modulate = new Color(rng.Randf(),rng.Randf(),rng.Randf());
            AddChild(panel);
        }
    }
}
