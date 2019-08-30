namespace SpaceEngine.Gravity
{
    public class Tree
    {
        public Node Root;

        private int dimension;

        public Tree(int dim = 2)
        {
            this.dimension = dim;
            this.Root = new Node { Childrens = new Node[dimension] };
        }
    }
}