using System;
using UnityEngine;

public class Tile
{
	/**
     * <summary>
     * Noise determines the type of the tile    
     * </summary>
     */
	private float noise;

	/**
     * <summary>
     * World position
     * </summary>
     */
	private Vector3 worldPosition;

	/**
     * <summary>
     * Relative position of each tile
     * </summary>
     */
	private Vector3 position;

	/**
     * <summary>
     * Moisture
     * </summary>
     */
	private float moisture;

	/**
     * <summary>
     * ID
     * </summary>
     */
	private int id;

	/**
     * <summary>
     * Set ID
     * </summary>
     *
     * <param name="id">int id</param>
     *
     * <returns>
     * void
     * </returns>
     */
	public void SetID(int id)
	{
		this.id = id;
	}

	/**
     * <summary>
     * Get ID
     * </summary>
     */
	public int GetID()
	{
		return this.id;
	}

	/**
     * <summary>
     * Set moisture
     * </summary>
     *
     * <param name="moisture">float moisture</param>
     *
     * <returns>
     * void
     * </returns>
     */
	public void SetMoisture(float moisture)
	{
		this.moisture = moisture;
	}

	/**
     * <summary>
     * Get moisture
     * </summary>
     *
     * <returns>
     * float this.moisture
     * </returns>
     */
	public float GetMoisture()
	{
		return this.moisture;
	}

	/**
     * <summary>
     * Set position
     * </summary>
     *
     * <param name="position">Vector2 position</param>
     * 
     * <returns>
     * void
     * </returns>
     */
	public void SetPosition(Vector3 position)
	{
		this.position = position;
	}

	/**
     * <summary>
     * Get position
     * </summary>
     *
     * <returns>
     * Vector2 this.position
     * </returns>
     */
	public Vector2 GetPosition()
	{
		return this.position;
	}

	/**
     * <summary>
     * Set world position
     * </summary>
     *
     * <param name="worldPosition">Vector3 worldPosition</param>
     *
     * <returns>
     * void
     * </returns>
     */
	public void SetWorldPosition(Vector3 worldPosition)
	{
		this.worldPosition = worldPosition;
	}

	/**
     * <summary>
     * Get world position
     * </summary>
     *
     * <returns>
     * Vector3 this.worldPosition
     * </returns>
     */
	public Vector3 GetWorldPosition()
	{
		return this.worldPosition;
	}


	/**
     * <summary>
     * Set noise    
     * </summary>
     * 
     * <param name="noise">float noise</param>
     * 
     * <returns>
     * void
     * </returns>
     */
	public void SetNoise(float noise)
	{
		this.noise = noise;
	}

	/**
     * <summary>
     * Get noise    
     * </summary>
     * 
     * <returns>
     * float this.noise
     * </returns>
     */
	public float GetNoise()
	{
		return this.noise;
	}
}
