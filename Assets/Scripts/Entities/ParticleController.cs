using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType {
	PLAYER_WALK,
	PLAYER_HIT,
	ENEMY_BULLET
}

[System.Serializable]
public class Particles {
	public ParticleType particleType;
	public bool EmitOnAwake;
	public bool IsInstantiated;
	public ParticleSystem PS;
}

public class ParticleController : MonoBehaviour {
	[SerializeField] private Particles[] particles;

	private Dictionary<ParticleType, ParticleSystem> particleInstantiatedDict = new Dictionary<ParticleType, ParticleSystem>();

	private void Start() {
		InitializeParticles();
	}

	private void InitializeParticles() {
		foreach(Particles particle in particles) {

			if(particle.IsInstantiated) {
				particleInstantiatedDict.Add(particle.particleType, particle.PS);
				break;
			}

			if(particle.EmitOnAwake) {
				particle.PS.enableEmission = true;
			} else {
				particle.PS.enableEmission = false;
			}
		}
	}

	public void ToggleEmitOnAllParticles(bool state) {
		foreach(Particles particle in particles) {
			particle.PS.enableEmission = state;
		}
	}

	public void ToggleEmitOnThisParticle(ParticleType type, bool state) {
		foreach(Particles particle in particles) {
			if(particle.particleType == type) {
				particle.PS.enableEmission = state;
				return;
			} else {
				Debug.Log("The particle of this type: " + type.ToString() + " does not exist on this object: " + gameObject.name);
			}
		}
	}

	public void InstantiateParticleOfType(ParticleType type, Vector2 pos) {
		if(particleInstantiatedDict.TryGetValue(type, out ParticleSystem ps)) {
			Instantiate(ps, pos, Quaternion.identity);
			return;
		}

		Debug.Log("The particle of this type: " + type.ToString() + " is not assigned on this object's ParticleController: " + gameObject.name);
	}
}
