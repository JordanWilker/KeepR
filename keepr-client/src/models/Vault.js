export class Vault {
  constructor(data = {}) {
    this.id = data.id || data._id || 'placeholder id'
    this.creatorId = data.creatorId || 'test creatorId'
    this.name = data.name || 'b'
    this.description = data.description
    this.isPrivate = data.isPrivate
  }
}
