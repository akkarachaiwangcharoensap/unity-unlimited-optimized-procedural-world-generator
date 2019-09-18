using UnityEngine;

public static class TileUVMap
{
	/**
     * <summary>
     * The total tiles
     * </summary>
     */
	public static float Total = 4f;

	/**
     * <summary>
     * The padding between each uvs tile
     * </summary>
     *
     */
	public static float Padding = 0.25f;

	/**
     * <summary>
     * UV mapping to the dirt texture
     * </summary>
     *
     * <returns>
     * Vector2[] UVs
     * </returns>
     */
	public static Vector2[] Dirt()
	{
		Vector2 init = new Vector2((0f + Padding) / Total, 0f + Padding);
		Vector2 final = new Vector2((1f - Padding) / Total, 1f - Padding);

		Vector2[] UVs = {
			new Vector2(init.x, init.y), // Bottom left corner
            new Vector2(final.x, init.y), // Bottom right corner
            new Vector2(final.x, final.y), // Top right corner
            new Vector2(init.x, final.y) // Top left corner
        };

		//return Vector2.zero;
		return UVs;
	}

    /**
     * <summary>
     * UV mapping to the grass texture
     * </summary>
     *
     * <returns>
     * Vector2[] UVs
     * </returns>
     */
    public static Vector2[] Grass()
	{
		Vector2 init = new Vector2((1f + Padding) / Total, 0f + Padding);
		Vector2 final = new Vector2((2f - Padding) / Total, 1f - Padding);

		Vector2[] UVs = {
			new Vector2(init.x, init.y), // Bottom left corner
            new Vector2(final.x, init.y), // Bottom right corner
            new Vector2(final.x, final.y), // Top right corner
            new Vector2(init.x, final.y), // Top left corner
        };

		return UVs;
	}

	/**
     * <summary>
     * UV mapping to the water texture
     * </summary>
     *
     * <returns>
     * Vector2[] UVs
     * </returns>
     */
	public static Vector2[] Water()
	{
		Vector2 init = new Vector2((2f + Padding) / Total, 0f + Padding);
		Vector2 final = new Vector2((3f - Padding) / Total, 1f - Padding);

		Vector2[] UVs = {
			new Vector2(init.x, init.y), // Bottom left corner
            new Vector2(final.x, init.y), // Bottom right corner
            new Vector2(final.x, final.y), // Top right corner
            new Vector2(init.x, final.y), // Top left corner
        };

		return UVs;
	}

	/**
     * <summary>
     * UV mapping to the sand texture
     * </summary>
     *
     * <returns>
     * Vector2[] UVs
     * </returns>
     */
	public static Vector2[] Sand()
	{
		Vector2 init = new Vector2((3f + Padding) / Total, 0f + Padding);
		Vector2 final = new Vector2((4f - Padding) / Total, 1f - Padding);

		Vector2[] UVs = {
			new Vector2(init.x, init.y), // Bottom left corner
            new Vector2(final.x, init.y), // Bottom right corner
            new Vector2(final.x, final.y), // Top right corner
            new Vector2(init.x, final.y), // Top left corner
        };

		return UVs;
	}
}
