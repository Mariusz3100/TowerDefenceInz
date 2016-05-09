using System;


	public struct PathfinderWeights
	{
		public static float predictionWeight=3f;
		public static float dynamicWeight=2f;
		public static float emptyFieldWeight=1f;
		public static float goalPointCost=0.1f;
		public static float staticWeight=(int)(MovementAdviceSystem.gameBounds.height*MovementAdviceSystem.gameBounds.width)*dynamicWeight;
		public static float borderWeight=float.MaxValue;
	}


