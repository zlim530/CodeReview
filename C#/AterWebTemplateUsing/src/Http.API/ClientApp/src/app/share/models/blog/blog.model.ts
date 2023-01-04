import { SystemUser } from '../system-user/system-user.model';
export interface Blog {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 标题
   */
  title?: string | null;
  /**
   * 概要
   */
  summary?: string | null;
  /**
   * 内容
   */
  content?: string | null;
  /**
   * 系统用户
   */
  systemUser?: SystemUser | null;

}
