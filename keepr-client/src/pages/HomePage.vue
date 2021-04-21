<template class="container">
  <div class="flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <h1 class="my-1 bg-dark text-light p-1 rounded d-flex align-items-center">
      KEEPR
    </h1>
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#createModal">
      Create
    </button>
    <div class="row">
      <div class="col card-columns">
        <keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import keep from '../components/keep.vue'

export default {
  components: { keep },
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => keepsService.getKeeps())
    return {
      state
    }
  }
}

</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
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
