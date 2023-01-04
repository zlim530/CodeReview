export interface BlogItemDto {
  /**
   * ����
   */
  title?: string | null;
  /**
   * ��Ҫ
   */
  summary?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;

}
