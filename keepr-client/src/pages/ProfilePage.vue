<template>
  <div class="container-fluid">
    <div class="profile row">
      <div class="col">
        {{ state.profile.name }}
      </div>
    </div>
    <div class="row">
      Vaults: {{ state.vaults.length }}
    </div>
    <div class="row">
      <div class=" col card-columns">
        <vault v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
      </div>
    </div>
    <div class="row">
      Keeps: {{ state.pkeeps.length }}
    </div>
    <div class="row">
      <div class="col card-columns">
        <pkeep v-for="pkeep in state.pkeeps" :key="pkeep.id" :pkeep="pkeep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import Pkeep from '../components/pkeep.vue'

export default {
  name: 'Profile',

  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultsService.getVaultsByProfileId(route.params.id)
      vaultsService.getProfileById(route.params.id)
      vaultsService.getKeepsByProfileId(route.params.id)
    })
    const state = reactive({
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeProfile),
      pkeeps: computed(() => AppState.pkeeps)
    })
    return {
      state
    }
  },
  components: { Pkeep }
}
</script>

<style lang="scss" scoped>
.card-columns {
  //small
  @media(max-width: 767px) {
    column-count: 2;
  }
  @media(min-width: 768px) {
    column-count: 3;
  }
  @media(min-width: 1200px) {
    column-count: 4;
  }
}
</style>
