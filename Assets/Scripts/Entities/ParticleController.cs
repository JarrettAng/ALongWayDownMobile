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
	[SerializeField] private Particles[] particles = default;

	private ParticleSystem.EmissionModule[] particlesEM;

	private Dictionary<ParticleType, ParticleSystem> particleInstantiatedDict = new Dictionary<ParticleType, ParticleSystem>();
	private Dictionary<ParticleType, ParticleSystem.EmissionModule> particleEmissionDict = new Dictionary<ParticleType, ParticleSystem.EmissionModule>();

    private void Awake() {
		InitializeParticles();
	}


	private void InitializeParticles() {
		foreach(Particles particle in particles) {

			if(particle.IsInstantiated) {
				particleInstantiatedDict.Add(particle.particleType, particle.PS);
				break;
			}

			particleEmissionDict.Add(particle.particleType, particle.PS.emission);

			if(particle.EmitOnAwake) {
				ToggleParticleSystem(particle.particleType, true);
			} else {
				ToggleParticleSystem(particle.particleType, false);
			}
		}
	}

	public void ToggleEmitOnAllParticles(bool state) {
		foreach(Particles particle in particles) {
			ToggleParticleSystem(particle.particleType, state);
		}
	}

	public void ToggleEmitOnThisParticle(ParticleType type, bool state) {
		ToggleParticleSystem(type, state);
	}

	public void InstantiateParticleOfType(ParticleType type, Vector2 pos) {
		if(particleInstantiatedDict.TryGetValue(type, out ParticleSystem ps)) {
			Instantiate(ps, pos, Quaternion.identity);
			return;
		}

		Debug.Log("The particle of this type: " + type.ToString() + " is not assigned on this object's ParticleController: " + gameObject.name);
	}

	private void ToggleParticleSystem(ParticleType particleType, bool state) {
		if(!particleEmissionDict.TryGetValue(particleType, out ParticleSystem.EmissionModule em)) {
			Debug.LogErrorFormat("The particle of this type: {0} does not exist on this object: {1}", particleType, gameObject.name);
			return;
        }

		em.enabled = state;
	}
}
