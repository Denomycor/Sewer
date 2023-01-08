using System.Collections.Generic;
using System;
using Godot;

public static class binarySpacePartitioning
{
    public static float deviation = 1;

    public static List<Partition> binarySpacePartition(Partition initPartition, int minLength, int minHeight, RandomNumberGenerator rng){
        //settup initial vars
        Queue<Partition> queue = new Queue<Partition>();
        List<Partition> list = new List<Partition>();
        queue.Enqueue(initPartition);

        //start partitioning
        while(queue.Count > 0){
            Partition current = queue.Dequeue();
            if(rng.Randf()<=0.5){
                //try to split horizontaly first
                //find random number centered in the center length and round it
                int n = (int)Math.Round(rng.Randfn(current.length/2,deviation/(current.length/2)));
                //check if it is bigger than the minimum length
                if(n>=minLength && n <= current.length-minLength){
                    //divide the partition into two new ones
                    Partition left = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY, n, current.height);
                    Partition right = new Partition(current.bottomLeftCornerX+n, current.bottomLeftCornerY, current.length-n, current.height);
                    //put them in queue to divide again
                    queue.Enqueue(left);
                    queue.Enqueue(right);
                }else{
                    //try to split vertically
                    n = (int)Math.Round(rng.Randfn(current.height/2,deviation/(current.height/2)));
                    if(n>=minHeight && n <= current.height-minHeight){
                    //divide the partition into two new ones
                    Partition bottom = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY, current.length, n);
                    Partition top = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY+n, current.length, current.height-n);
                    //put them in queue to divide again
                    queue.Enqueue(bottom);
                    queue.Enqueue(top);
                    }else{
                        //cant divide any further
                        list.Add(current);
                    }
                }
            }else{
                //try to split vertically first
                //find random number centered in the center length and round it
                int n = (int)Math.Round(rng.Randfn(current.height/2,deviation/(current.height/2)));
                //check if it is bigger than the minimum height
                if(n>=minHeight && n <= current.height-minHeight){
                //divide the partition into two new ones
                Partition bottom = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY, current.length, n);
                Partition top = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY+n, current.length, current.height-n);
                //put them in queue to divide again
                queue.Enqueue(bottom);
                queue.Enqueue(top);
                }else{
                    //try to split horizontally
                    n = (int)Math.Round(rng.Randfn(current.length/2,deviation/(current.length/2)));
                    if(n>=minLength && n <= current.length-minLength){
                    //divide the partition into two new ones
                    Partition left = new Partition(current.bottomLeftCornerX, current.bottomLeftCornerY, n, current.height);
                    Partition right = new Partition(current.bottomLeftCornerX+n, current.bottomLeftCornerY, current.length-n, current.height);
                    //put them in queue to divide again
                    queue.Enqueue(left);
                    queue.Enqueue(right);
                    }else{
                        //cant divide any further
                        list.Add(current);
                    }
                }
            }
        }

        return list;
    }
}

/*
 * class defining a partition
*/
public class Partition
{
    public int bottomLeftCornerX;
    public int bottomLeftCornerY;
    public int length;
    public int height;

    public Partition(int bottomLeftCornerX, int bottomLeftCornerY, int length, int height){
        this.bottomLeftCornerX = bottomLeftCornerX;
        this.bottomLeftCornerY = bottomLeftCornerY;
        this.length = length;
        this.height = height;
    } 
}
