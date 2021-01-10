<template>
  <div>
    <img id="main" src="../../assets/concert.gif">
    <h1>You will ride eternal, shiny and chrome!</h1>
    <concertSelection></concertSelection>
  </div>
</template>

<script>
import concertSelection from "./ConcertSelector"

export default {
  components: { concertSelection }
};
</script>

<style lang="css" scoped>
img#main{
  width: 100%;
}
</style>
