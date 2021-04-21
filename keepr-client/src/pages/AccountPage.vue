<template>
  <div class="about text-center container-fluid">
    <h1 class="row">
      Welcome {{ account.name }}
    </h1>
    <div class="row">
      Vaults: {{ state.vaults.length }}
    </div>
    <div class="row">
      Keeps: {{ state.pkeeps.length }}
    </div>
    <div class="row">
      YOUR VAULTS
    </div>
    <div class="row">
      <div class="card-columns col">
        <vault v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
      </div>
      <i class="fa fa-plus fa-3x" data-toggle="modal" data-target="#createModalVault">
      </i>
    </div>
    <div class="row">
      YOUR KEEPS
    </div>
    <div class="row">
      <div class="card-columns col">
        <pkeep v-for="pkeep in state.pkeeps" :key="pkeep.id" :pkeep="pkeep" />
      </div>
      <i class="fa fa-plus fa-3x" data-toggle="modal" data-target="#createModal">
      </i>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Account',
  setup() {
    onMounted(() => {
      keepsService.getKeepsByAccountId()
    })
    const state = reactive({
      pkeeps: computed(() => AppState.userKeeps),
      vaults: computed(() => AppState.userVaults)
    })
    return {
      account: computed(() => AppState.account),
      state
    }
  }
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
